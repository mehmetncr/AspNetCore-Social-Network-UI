

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


        localStorage.setItem('newMessage', true);
        document.getElementById('notificationSpan').style.display = 'inline-block';

        if (newMessages.length > 5) {
            newMessages.splice(0, 1);
        }
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




        var messageBoxTargetUserId = $('#targetUserId').val();

        if (senderUserId == messageBoxTargetUserId) {

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
                    messageHtml += '<li class="me">';
                    messageHtml += '<div class="chat-thumb"><img src="' + chat.user.userProfilePicture + '" alt=""></div>';
                    messageHtml += ' <div class="notification-event">'
                    messageHtml += '<span class="chat-message-item" style="width:100%;">' + message + '</span> <span class="notification-date"><time datetime="2004-07-24T18:18" class="entry-date updated">' + new Date().toLocaleString('tr-TR') + '</time></span>';
                    messageHtml += '</div>'
                    messageHtml += '</li>';

                    $("#messageBox").append(messageHtml);
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



});

document.addEventListener("DOMContentLoaded", function () {
    var newMessageNotification = localStorage.getItem('newMessage');
    console.log(newMessageNotification);
    if (newMessageNotification == "true") {
        document.getElementById('notificationSpan').style.display = 'inline-block';
    };

});




document.addEventListener("DOMContentLoaded", function () {
    var infoCount = noti.length;
    var spanName = document.getElementById('notifiCount');
    var text = document.createTextNode(infoCount);
    spanName.appendChild(text);
    var spanText = document.getElementById('notifiCountText');
    var textinfo = document.createTextNode(infoCount + " Yeni Bildirim");
    spanText.appendChild(textinfo);

    var ulelement = document.getElementById('notificationList');

    noti.forEach(function (notification) {

        var lielement = document.createElement("li");
        lielement.innerHTML = `
                <a href="#" onclick="viewPost('${notification.notificationDescription}')" title="">
                    <img src="${notification.notificationSenderUser.userProfilePicture}" alt="">
                    <div class="mesg-meta">
                        <h6>${notification.notificationTitle}</h6>
                        <span>${notification.notificationSenderUser.userFirstName} ${notification.notificationSenderUser.userLastName}</span>
                        <i>2 min ago</i>
                    </div>
                </a>
            
            `;
        ulelement.appendChild(lielement);
    });



});

function viewPost(id) {
    alert(id);
}

connection.on("ReceivePostNotifRes", (notification) => {

    debugger;
    var spanName = document.getElementById('notifiCount');
    var count = parseInt(spanName.innerText) + 1;
    spanName.innerText = '';
    var text = document.createTextNode(count);
    spanName.appendChild(text);

    var spanText = document.getElementById('notifiCountText');
    spanText.innerText = '';
    var textinfo = document.createTextNode(count + " Yeni Bildirim");
    spanText.appendChild(textinfo);


    var ulElement = document.getElementById('notificationList');

    var newLiElement = document.createElement('li');
    newLiElement.innerHTML = `
        <a href="notifications.html" title="">
                    <img src="${notification.notificationSenderUser.userProfilePicture}" alt="">
                    <div class="mesg-meta">
                        <h6>${notification.notificationTitle}</h6>
                        <span>${notification.notificationSenderUser.userFirstName} ${notification.notificationSenderUser.userLastName}</span>
                        <i>2 min ago</i>
                    </div>
                </a>
              
            `;
    ulElement.appendChild(newLiElement);
});


connection.on("ReceiveFriendReq", (notification) => {


    var spanName = document.getElementById('notifiCount');
    var count = parseInt(spanName.innerText) + 1;
    spanName.innerText = '';
    var text = document.createTextNode(count);
    spanName.appendChild(text);

    var spanText = document.getElementById('notifiCountText');
    spanText.innerText = '';
    var textinfo = document.createTextNode(count + " Yeni Bildirim");
    spanText.appendChild(textinfo);


    var ulElement = document.getElementById('notificationList');

    var newLiElement = document.createElement('li');
    newLiElement.innerHTML = `
        <a href="notifications.html" title="">
                    <img src="${notification.notificationSenderUser.userProfilePicture}" alt="">
                    <div class="mesg-meta">
                        <h6>${notification.notificationTitle}</h6>
                        <span>${notification.notificationSenderUser.userFirstName} ${notification.notificationSenderUser.userLastName}</span>
                        <i>2 min ago</i>
                    </div>
                </a>
              
            `;
    ulElement.appendChild(newLiElement);
});

function deleteNotification() {   
    $.ajax({
        type: "GET",
        url: "/Notification/DeleteNotification/",   
        success: function (result) {   
            
        },
        error: function () {        
        }
    });
}