function postLike(postId, likeCount) {
    var likeIcon = document.getElementById(`likeIcon${postId}`)
    var likeCountElement = document.getElementById(`like${postId}`)

    if (likeIcon.classList.contains('ti-heart')) {
        $.ajax({
            type: "POST",
            url: "/Post/PostLike/",
            data: { postId: postId },
            success: function (result) {
                console.log('result' + result);
                console.log(likeCount);
                likeIcon.classList.remove("ti-heart");
                likeIcon.classList.add("fa", "fa-heart");
                likeCountElement.innerHTML = result;
            },
            error: function () {
                // Hata durumunda yapılacak işlemler
            }
        });
    }
    else {
        $.ajax({
            type: "POST",
            url: "/Post/PostTakeBackLike/",
            data: { postId: postId },
            success: function (result) {
                console.log('result' + result);
                console.log(likeCount);
                likeIcon.classList.remove("fa", "fa-heart");
                likeIcon.classList.add("ti-heart");
                likeCountElement.innerHTML = result;
            },
            error: function () {
                // Hata durumunda yapılacak işlemler
            }
        });

    }
}



function postDislike(postId) {
    $.ajax({
        type: "POST",
        url: "/Post/PostLike/",
        data: { postId: postId },
        success: function (result) {
            // Beğeni sayısını güncelle
            $("#likeCount").html(result);
        },
        error: function () {

        }
    });
}

function replyToComment(commentId, commentContent, firstName, lastName, postId) {
    // Yanıtlanan yorumun içeriğini ve ID'sini form içine ekleyin
    console.log(postId);
    var replyToArea = document.getElementById(`replyArea${postId}`);
    if (replyToArea.style.display === "none") {
        replyToArea.style.display = "block";
    }
    console.log(replyToArea);
    $(`#commentContent${postId}`).text(commentContent);
    $(`#commentUser${postId}`).text('Cevap Veriliyor: ' + firstName + ' ' + lastName);
    $(`#parentComment${postId}`).val(commentId);

    // Formun görünürlüğünü sağlayın (gerekirse)
    // Örneğin: $('#addcomment2').show();
}
function closeReplyToArea(postId) {
    var replyToArea = document.getElementById(`replyArea${postId}`);
    if (replyToArea.style.display === "block") {
        replyToArea.style.display = "none";
        $(`#parentComment${postId}`).val("");
    }
}

function youtube() {
    var input = document.getElementById("youtube-input");
    if (input.style.display === "none") {
        input.style.display = "block";
    }
    else {
        input.style.display = "none";
    }
}
document.getElementById('fileInput').addEventListener('change', function () {
    var uploadAnimation = document.getElementById('uploadAnimation');
    var postButton = document.getElementById('postButton');

    // Dosya yükleme simülasyonu (Gerçek bir yükleme işlemi için sunucu tarafı kodu eklenmelidir)
    uploadAnimation.style.display = 'block';
    setTimeout(function () {
        uploadAnimation.style.display = 'none';
        postButton.removeAttribute('disabled');
    }, 2000); // 2 saniye sonra simülasyonu tamamla
});
function showVideoThumbnail(input) {
    var fileInput = input;
    var file = fileInput.files[0];

    if (file) {
        var video = document.createElement("video");
        video.src = URL.createObjectURL(file);
        video.width = 150; // Resmin genişliğini isteğe bağlı olarak ayarlayabilirsiniz
        video.height = 75; // Resmin yüksekliğini isteğe bağlı olarak ayarlayabilirsiniz

        var videoThumb = document.getElementById("videoThumb");
        videoThumb.innerHTML = "";
        videoThumb.appendChild(video);

        video.addEventListener("loadeddata", function () {
            URL.revokeObjectURL(video.src);
        });
        var videoThumbnail = document.getElementById('videoThumbContainer');
        videoThumbnail.style.display = "block";
    }
}
function deleteVideo() {
    $('#fileInput').val('');
    $('#imageFile').val('');
    var videoThumb = document.getElementById("videoThumb");
    videoThumb.innerHTML = "";
    var videoThumb = document.getElementById('videoThumbContainer');
    videoThumb.style.display = "none";
}
function showImageThumbnail(input) {
    var fileInput = input;
    var file = fileInput.files[0];

    if (file) {
        var image = document.createElement("img"); // "img" elementi, resim göstermek için kullanılmalıdır
        image.src = URL.createObjectURL(file);
        image.height = 75; // Resmin yüksekliğini isteğe bağlı olarak ayarlayabilirsiniz

        var imageThumb = document.getElementById("videoThumb");
        imageThumb.innerHTML = "";
        imageThumb.appendChild(image);

        image.addEventListener("load", function () {
            URL.revokeObjectURL(image.src); // Bellek sızıntısını önlemek için
        });

        var imageThumbnailContainer = document.getElementById('videoThumbContainer');
        imageThumbnailContainer.style.display = "block";
    }
}
