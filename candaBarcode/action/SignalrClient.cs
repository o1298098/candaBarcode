using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace candaBarcode.action
{

    public class SignalrClient
    {
        private readonly HubConnection _connection;
        private readonly IHubProxy _proxy;//客户端代理服务器端中心  
        public event EventHandler<string[]> OnReceiveEvent; //定义一个接收server端的事件  
        public SignalrClient()
        {
            _connection = new HubConnection("http://120.76.230.35:1886/");
            _proxy = _connection.CreateHubProxy("ChatHub");
            _proxy.On("addNewMessageToPage", (string user, string message,string Latitude, string Longitude) =>
            {
                OnReceiveEvent(this,new string[]{user,message, Latitude, Longitude});
            });
            _connection.StateChanged += _connection_StateChanged;



        }

        private async void _connection_StateChanged(StateChange obj)
        {
            if (_connection.State == ConnectionState.Disconnected)
            {
                await _connection.Start();
            }
        }

        public async Task Connect()
        {
            await _connection.Start();
        }

        public async Task Send(string user,string message, string Latitude, string Longitude)
        {
            string serverMethod = "Send"; //serverHub中定义的server端方法名称  
            await _proxy.Invoke(serverMethod, new object[] { user, message , Latitude, Longitude});//Invode the 'SendMessage' method on ther server   
        }
    }

}
