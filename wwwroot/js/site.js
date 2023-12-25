function postLike(postId, userId) {
    var likeIcon = document.getElementById(`likeIcon${postId}`);
    var likeCountElement = document.getElementById(`like${postId}`);
    var localLikedPosts = JSON.parse(localStorage.getItem('likedPosts'));
    var localDislikedPosts = JSON.parse(localStorage.getItem('dislikedPosts'));

    if (localDislikedPosts != null) {
        localDislikedPosts.forEach(function (item) {
            if (item.postId == postId && item.isDislike == true) {
                postTakeBackDislike(postId);
            }
        })
    }
    

    if (likeIcon.classList.contains('fa-regular')) {
        $.ajax({
            type: "POST",
            url: "/Post/PostLike/",
            data: { postId: postId },
            success: function (result) {
                likeIcon.classList.remove("fa-regular");
                likeIcon.classList.add("fa-solid");
                likeCountElement.innerHTML = result;
                console.log(userId);
                
                postNotification(userId, "Like");
                
                var newLikedPost = {
                    postId: postId,
                    isLike: true
                };

                if (localLikedPosts == null) {
                    localStorage.setItem('likedPosts', JSON.stringify([newLikedPost]));
                } else {
                    var postExists = false;

                    for (var i = 0; i < localLikedPosts.length; i++) {
                        if (localLikedPosts[i].postId == postId) {
                            localLikedPosts[i].isLike = true;
                            localStorage.setItem('likedPosts', JSON.stringify(localLikedPosts));
                            postExists = true;
                            break;
                        }
                    }

                    if (!postExists) {
                        localLikedPosts.push(newLikedPost);
                        localStorage.setItem('likedPosts', JSON.stringify(localLikedPosts));
                    }
                }
            },
            error: function () {
                // Hata durumunda yapılacak işlemler
            }
        });
    } else {
        // Kullanıcı daha önce like atmışsa, like'ı geri al
        postTakeBackLike(postId);
    }
}

function postTakeBackLike(postId) {
    var likeIcon = document.getElementById(`likeIcon${postId}`);
    var likeCountElement = document.getElementById(`like${postId}`);
    var localLikedPosts = JSON.parse(localStorage.getItem('likedPosts'));

    if (likeIcon.classList.contains('fa-solid')) {
        $.ajax({
            type: "POST",
            url: "/Post/PostTakeBackLike/",
            data: { postId: postId },
            success: function (result) {
                likeIcon.classList.remove("fa-solid");
                likeIcon.classList.add("fa-regular");
                likeCountElement.innerHTML = result;

                // Eğer post zaten varsa, like'ı geri al
                if (localLikedPosts != null) {
                    for (var i = 0; i < localLikedPosts.length; i++) {
                        if (localLikedPosts[i].postId == postId) {
                            localLikedPosts[i].isLike = false;
                            localStorage.setItem('likedPosts', JSON.stringify(localLikedPosts));
                            break;
                        }
                    }
                }
            },
            error: function () {
                // Hata durumunda yapılacak işlemler
            }
        });
    }
}

function postNotification(userId, notificationType) {
    connection.invoke("PostNotification", userId,notificationType);
}

function postDislike(postId, userId) {
    var dislikeIcon = document.getElementById(`dislikeIcon${postId}`);
    var dislikeCountElement = document.getElementById(`dislike${postId}`);
    var localDislikedPosts = JSON.parse(localStorage.getItem('dislikedPosts'));
    var localLikedPosts = JSON.parse(localStorage.getItem('likedPosts'));
    if (localLikedPosts != null) {
        localLikedPosts.forEach(function (item) {
            if (item.postId == postId && item.isLike == true) {
                postTakeBackLike(postId);
            }
        })
    }
    



    if (dislikeIcon.classList.contains('fa-regular')) {
        $.ajax({
            type: "POST",
            url: "/Post/PostDislike/",
            data: { postId: postId },
            success: function (result) {
                console.log('result' + result);
                dislikeIcon.classList.remove("fa-regular");
                dislikeIcon.classList.add("fa-solid");
                dislikeCountElement.innerHTML = result;
                postNotification(userId, "Dislike");
                var newDislikedPost = {
                    postId: postId,
                    isDislike: true
                };

                if (localDislikedPosts == null) {
                    localStorage.setItem('dislikedPosts', JSON.stringify([newDislikedPost]));
                } else {
                    var postExists = false;

                    for (var i = 0; i < localDislikedPosts.length; i++) {
                        if (localDislikedPosts[i].postId == postId) {
                            localDislikedPosts[i].isDislike = true;
                            localStorage.setItem('dislikedPosts', JSON.stringify(localDislikedPosts));
                            postExists = true;
                            break;
                        }
                    }

                    if (!postExists) {
                        localDislikedPosts.push(newDislikedPost);
                        localStorage.setItem('dislikedPosts', JSON.stringify(localDislikedPosts));
                    }
                }
            },
            error: function () {
                // Hata durumunda yapılacak işlemler
            }
        });
    } else {
        // Kullanıcı daha önce dislike atmışsa, dislike'ı geri al
        postTakeBackDislike(postId);
    }
}

function postTakeBackDislike(postId) {
    var dislikeIcon = document.getElementById(`dislikeIcon${postId}`);
    var dislikeCountElement = document.getElementById(`dislike${postId}`);
    var localDislikedPosts = JSON.parse(localStorage.getItem('dislikedPosts'));

    if (dislikeIcon.classList.contains('fa-solid')) {
        $.ajax({
            type: "POST",
            url: "/Post/PostTakeBackDislike/",
            data: { postId: postId },
            success: function (result) {
                dislikeIcon.classList.remove("fa-solid");
                dislikeIcon.classList.add("fa-regular");
                dislikeCountElement.innerHTML = result;

                // Eğer post zaten varsa, dislike'ı geri al
                if (localDislikedPosts != null) {
                    for (var i = 0; i < localDislikedPosts.length; i++) {
                        if (localDislikedPosts[i].postId == postId) {
                            localDislikedPosts[i].isDislike = false;
                            localStorage.setItem('dislikedPosts', JSON.stringify(localDislikedPosts));
                            break;
                        }
                    }
                }
            },
            error: function () {
                // Hata durumunda yapılacak işlemler
            }
        });
    }
}


function replyToComment(commentId, commentContent, firstName, lastName, postId) {

    console.log(postId);
    var replyToArea = document.getElementById(`replyArea${postId}`);
    if (replyToArea.style.display === "none") {
        replyToArea.style.display = "block";
    }
    console.log(replyToArea);
    $(`#commentContent${postId}`).text(commentContent);
    $(`#commentUser${postId}`).text('Cevap Veriliyor: ' + firstName + ' ' + lastName);
    $(`#parentComment${postId}`).val(commentId);

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


    uploadAnimation.style.display = 'block';
    setTimeout(function () {
        uploadAnimation.style.display = 'none';
        postButton.removeAttribute('disabled');
    }, 2000); 
});
function showVideoThumbnail(input) {
    var fileInput = input;
    var file = fileInput.files[0];

    if (file) {
        var video = document.createElement("video");
        video.src = URL.createObjectURL(file);
        video.width = 150; 
        video.height = 75; 

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
        var image = document.createElement("img"); 
        image.src = URL.createObjectURL(file);
        image.height = 75; 

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
