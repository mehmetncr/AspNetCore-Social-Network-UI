
function addFriend(userId) {
    $.ajax({
        type: "POST",
        url: "/Friend/AddFriend/",
        data: { userId: userId },
        success: function (result) {
         
            Swal.fire({
                position: "top-end",
                icon: "success",
                title: "Arkadaşlık İsteği Gönderildi",
                showConfirmButton: false,
                timer: 900
            });
        }
    })
}