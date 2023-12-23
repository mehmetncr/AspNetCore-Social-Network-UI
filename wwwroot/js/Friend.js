
function addFriend(userId) {  
    connection.invoke("FriendReqSend", userId);
}


connection.on("ReceiveFriendReqRes", (result) => {
    if (result == "Ok") {
        Swal.fire({
            position: "top-end",
            icon: "success",
            title: "Arkadaşlık İsteği Gönderildi",
            showConfirmButton: false,
            timer: 900
        });
    }
    else if (result == "AlreadySend") {
        Swal.fire({
            position: "top-end",
            icon: "error",
            title: "Zaten Arkadaşlık İsteği Göderilmiş ",
            showConfirmButton: false,
            timer: 900
        });
    }
})