﻿

//------------------------------------------WebSockets-------------------------------------------------------------

"use strict";
var newMessages = [];

//gelen mesajları dinler
connection.on("ReceivePrivateMessage", (senderUserId, message) => {

    if (document.URL == "https://localhost:7184/Message/MyMessages") {

        var targetUser = $('#targetUserId').val();
        console.log(targetUser + ' ' + senderUserId);
        //eğer gelen mesaj açık olan bir konuşmanın ise konuşmaya eklenir
        if (senderUserId == targetUser) {


            model.forEach(function (chat) {

                if (chat.user.userId == senderUserId) {

                    var newMessage = {
                        sendDate: Date.now(),
                        messageContent: message,
                        messageId: chat.messageId,
                        ownerUserId: senderUserId
                    };

                    chat.messageDetails.push(newMessage);

                    var messageHtml = '';
                    messageHtml += '<li class="you">';
                    messageHtml += '<figure><img src="' + chat.user.userProfilePicture + '" alt=""></figure>';
                    messageHtml += '<p>' + message + '<span style="float:right; font-size:12px;">' + new Date().toLocaleString('tr-TR') + '</span></p>';
                    messageHtml += '</li>';

                    $(".chatting-area").append(messageHtml);
                    scrollToBottom();
                }
            })

        }
        else {
            // eğer konuşma kapalı ise mesajlara eklenir
            model.forEach(function (chat) {

                if (chat.user.userId == senderUserId) {

                    var newMessage = {
                        sendDate: new Date(),
                        messageContent: message,
                        messageId: chat.messageId,
                        ownerUserId: senderUserId
                    };

                    chat.messageDetails.push(newMessage);
                }
            })
        }
    }
    else {


        var newMessageInfo = {
            messageContent: message,
            senderUserId: senderUserId
        }
        newMessages.push(newMessageInfo);
        var messagesList = document.getElementById('messagesList');
        messagesList.innerHTML = "";
        newMessages.forEach(function (message) {

            var newLi = document.createElement('li');

            newLi.innerHTML = `<a href="notifications.html" title="">
                <img src="message.userphoto" alt="">
                <div class="mesg-meta">
                    <h6>${message.senderUserId}</h6>
                    <span>${message.messageContent}</span>
                    <i>2 min ago</i>
                </div>
            </a>
            <span class="tag green">New</span>`;
            messagesList.appendChild(newLi);
        });
    }
});


//SignalR bağlantısı sağlanır


//mesaj gönderim işlemi
//function send(event) {
//    var message = $('#textMessage').val()
//    event.preventDefault();
//    connection.invoke("MessageSend", message, $('#targetUserId').val(), $('#messageId').val());


//}

////gelen mesajları dinler
//connection.on("ReceivePrivateMessage", (senderUserId, message) => {
//    cconsole.log(message);

//});





//window.addEventListener('beforeunload', function () {
//    connection.invoke('DisconnectOnUnload').catch(function (err) {
//        return console.error(err.toString());
//    });
//});
//------------------------------------------WebSockets-------------------------------------------------------------