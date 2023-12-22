//------------------------------------------WebSockets-------------------------------------------------------------



//SignalR baðlantýsý saðlanýr

var connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7091/MessageHub", {
        accessTokenFactory: () => token
    })
    .build();



connection.start().then(() => {
    console.log("Baðlantý kimliði:", connection.connectionId);
}).catch((err) => {
    console.log('Hata:', err.toString());
});


window.addEventListener('beforeunload', function () {
    connection.invoke('DisconnectOnUnload').catch(function (err) {
        return console.error(err.toString());
    });
});

