jQuery(document).ready(function ($) {


    "use strict";

    //------- Notifications Dropdowns
    $('.top-area > .setting-area > li').on("click", function () {
        $(this).siblings().children('div').removeClass('active');
        $(this).children('div').addClass('active');
        return false;
    });
    //------- remove class active on body
    $("body *").not('.top-area > .setting-area > li').on("click", function () {
        $(".top-area > .setting-area > li > div").removeClass('active');
    });


    //--- user setting dropdown on topbar	
    $('.user-img').on('click', function () {
        $('.user-setting').toggleClass("active");
        return false;
    });

    //--- side message box	
    $('.friendz-list > li, .chat-users > li').on('click', function () {
        $('.chat-box').addClass("show");
        return false;
    });
    $('.close-mesage').on('click', function () {
        $('.chat-box').removeClass("show");
        return false;
    });

    //------ scrollbar plugin
    if ($.isFunction($.fn.perfectScrollbar)) {
        $('.dropdowns, .twiter-feed, .invition, .followers, .chatting-area, .peoples, #people-list, .chat-list > ul, .message-list, .chat-users, .left-menu').perfectScrollbar();
    }

    /*--- socials menu scritp ---*/
    $('.trigger').on("click", function () {
        $(this).parent(".menu").toggleClass("active");
    });

    /*--- emojies show on text area ---*/
    $('.add-smiles > span').on("click", function () {
        $(this).parent().siblings(".smiles-bunch").toggleClass("active");
    });

    // delete notifications
    $('.notification-box > ul li > i.del').on("click", function () {
        $(this).parent().slideUp();
        return false;
    });

    /*--- socials menu scritp ---*/
    $('.f-page > figure i').on("click", function () {
        $(".drop").toggleClass("active");
    });

    //===== Search Filter =====//
    (function ($) {
        // custom css expression for a case-insensitive contains()
        jQuery.expr[':'].Contains = function (a, i, m) {
            return (a.textContent || a.innerText || "").toUpperCase().indexOf(m[3].toUpperCase()) >= 0;
        };

        function listFilter(searchDir, list) {
            var form = $("<form>").attr({ "class": "filterform", "action": "#" }),
                input = $("<input>").attr({ "class": "filterinput", "type": "text", "placeholder": "Search Contacts..." });
            $(form).append(input).appendTo(searchDir);

            $(input)
                .change(function () {
                    var filter = $(this).val();
                    if (filter) {
                        $(list).find("li:not(:Contains(" + filter + "))").slideUp();
                        $(list).find("li:Contains(" + filter + ")").slideDown();
                    } else {
                        $(list).find("li").slideDown();
                    }
                    return false;
                })
                .keyup(function () {
                    $(this).change();
                });
        }

        //search friends widget
        $(function () {
            listFilter($("#searchDir"), $("#people-list"));
        });
    }(jQuery));

    //progress line for page loader
    $('body').show();
    NProgress.start();
    setTimeout(function () { NProgress.done(); $('.fade').removeClass('out'); }, 2000);

    //--- bootstrap tooltip	
    $(function () {
        $('[data-toggle="tooltip"]').tooltip();
    });

    // Sticky Sidebar & header
    if ($(window).width() < 769) {
        jQuery(".sidebar").children().removeClass("stick-widget");
    }

    if ($.isFunction($.fn.stick_in_parent)) {
        $('.stick-widget').stick_in_parent({
            parent: '#page-contents',
            offset_top: 60,
        });


        $('.stick').stick_in_parent({
            parent: 'body',
            offset_top: 0,
        });

    }

    /*--- topbar setting dropdown ---*/
    $(".we-page-setting").on("click", function () {
        $(".wesetting-dropdown").toggleClass("active");
    });

    /*--- topbar toogle setting dropdown ---*/
    $('#nightmode').on('change', function () {
        if ($(this).is(':checked')) {
            // Show popup window
            $(".theme-layout").addClass('black');
        }
        else {
            $(".theme-layout").removeClass("black");
        }
    });

    //chosen select plugin
    if ($.isFunction($.fn.chosen)) {
        $("select").chosen();
    }

    //----- add item plus minus button
    if ($.isFunction($.fn.userincr)) {
        $(".manual-adjust").userincr({
            buttonlabels: { 'dec': '-', 'inc': '+' },
        }).data({ 'min': 0, 'max': 20, 'step': 1 });
    }

    if ($.isFunction($.fn.loadMoreResults)) {
        $('.loadMore').loadMoreResults({
            displayedItems: 3,
            showItems: 1,
            button: {
                'class': 'btn-load-more',
                'text': 'Load More'
            }
        });
    }
    //===== owl carousel  =====//
    if ($.isFunction($.fn.owlCarousel)) {
        $('.sponsor-logo').owlCarousel({
            items: 6,
            loop: true,
            margin: 30,
            autoplay: true,
            autoplayTimeout: 1500,
            smartSpeed: 1000,
            autoplayHoverPause: true,
            nav: false,
            dots: false,
            responsiveClass: true,
            responsive: {
                0: {
                    items: 3,
                },
                600: {
                    items: 3,

                },
                1000: {
                    items: 6,
                }
            }

        });
    }

    // slick carousel for detail page
    if ($.isFunction($.fn.slick)) {
        $('.slider-for-gold').slick({
            slidesToShow: 1,
            slidesToScroll: 1,
            arrows: false,
            slide: 'li',
            fade: false,
            asNavFor: '.slider-nav-gold'
        });

        $('.slider-nav-gold').slick({
            slidesToShow: 3,
            slidesToScroll: 1,
            asNavFor: '.slider-for-gold',
            dots: false,
            arrows: true,
            slide: 'li',
            vertical: true,
            centerMode: true,
            centerPadding: '0',
            focusOnSelect: true,
            responsive: [
                {
                    breakpoint: 768,
                    settings: {
                        slidesToShow: 3,
                        slidesToScroll: 1,
                        infinite: true,
                        vertical: false,
                        centerMode: true,
                        dots: false,
                        arrows: false
                    }
                },
                {
                    breakpoint: 641,
                    settings: {
                        slidesToShow: 3,
                        slidesToScroll: 1,
                        infinite: true,
                        vertical: true,
                        centerMode: true,
                        dots: false,
                        arrows: false
                    }
                },
                {
                    breakpoint: 420,
                    settings: {
                        slidesToShow: 3,
                        slidesToScroll: 1,
                        infinite: true,
                        vertical: false,
                        centerMode: true,
                        dots: false,
                        arrows: false
                    }
                }
            ]
        });
    }

    //---- responsive header

    $(function () {

        //	create the menus
        $('#menu').mmenu();
        $('#shoppingbag').mmenu({
            navbar: {
                title: 'General Setting'
            },
            offCanvas: {
                position: 'right'
            }
        });

        //	fire the plugin
        $('.mh-head.first').mhead({
            scroll: {
                hide: 200
            }

        });
        $('.mh-head.second').mhead({
            scroll: false
        });


    });

    //**** Slide Panel Toggle ***//
    $("span.main-menu").on("click", function () {
        $(".side-panel").addClass('active');
        $(".theme-layout").addClass('active');
        return false;
    });

    $('.theme-layout').on("click", function () {
        $(this).removeClass('active');
        $(".side-panel").removeClass('active');


    });


    // login & register form
    //$('button.signup').on("click", function () {
    //	$('.login-reg-bg').addClass('show');
    //	return false;
    //});

    //$('.already-have').on("click", function () {
    //	$('.login-reg-bg').removeClass('show');
    //	return false;
    //});

    //----- count down timer		
    if ($.isFunction($.fn.downCount)) {
        $('.countdown').downCount({
            date: '11/12/2018 12:00:00',
            offset: +10
        });
    }

    /** Post a Comment **/
    $(".post-comt-box textarea").on("keydown", function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            var form = $(this).closest("form");
            submitForm(form);
        }
    });

    function submitForm(form) {
        var postId = form.find("input[name='postId']").val();
        var postOwnerUserId = form.find("input[name='postOwnerUserId']").val();

        // Simüle edilmiş bir POST isteği
        $.ajax({
            type: "POST",
            url: "/Post/AddComment",
            data: form.serialize(), // Form verilerini al
            success: function (result) {
                // Başarılı yanıt alındığında yeni yorumu sayfaya ekle
            
                var commentContainer = $(`#${postId}`);
                commentContainer.empty();
                postNotification(postOwnerUserId, "Comment", postId);       //Yorum yapıldığında post sahibine bildirim gönderme işlemi
                result.forEach(function (item) {
                    form.find("textarea[name='comment']").val('');
                    var date = new Date(item.commentDate);
                    var gun = date.getDate();
                    var ayIndex = date.getMonth();
                    var yil = date.getFullYear();
                    var saat = date.toLocaleTimeString();
                    var tamTarih = gun + "." + ayIndex + "." + yil +" "+ saat;

                    var newComment = `
                
                    <li>
                        <div class="comet-avatar">
                            <img width="50" src="${item.commentUser.userProfilePicture}" alt="">
                        </div>
                        <div class="we-comment" style="box-shadow:1px 1px 5px gray">
                            <div class="coment-head">
                                <h5><a href="time-line.html" title="">${item.commentUser.userFirstName} ${item.commentUser.userLastName}</a></h5>
                                <span>${tamTarih}</span>
                                <a class="we-reply" href="javascript:void(0)" title="Reply" onclick="replyToComment('${item.commentId}', '${item.commentContent}', '${item.commentUser.userFirstName}', '${item.commentUser.userLastName}', '${item.commentPostId}', 'commentContent${postId}', 'commentUser${postId}', 'parentComment${postId}', 'replyArea${postId}');"><i class="fa fa-reply"></i></a>
                            </div>
                            <p>${item.commentContent}</p>
                            </div>
                            <ul>
                                ${item.replyComments && item.replyComments.length > 0 ? item.replyComments.map(function (reply) {
                                    var replyDate = new Date(reply.replyCommentDate);
                                    var replyDay = replyDate.getDate();
                                    var replyMonth = date.getMonth();
                                    var replyYear = date.getFullYear();
                                    var replyHour = date.toLocaleTimeString();
                                    var replyFullDate = replyDay + "." + replyMonth + "." + replyYear + " " + replyHour;
                        return `
                                        <li>
                                            <div class="comet-avatar">
                                                <img width="50" src="${reply.replyCommentUser.userProfilePicture}" alt="">
                                            </div>
                                            <div class="we-comment" style="box-shadow:1px 1px 5px gray">
                                                <div class="coment-head">
                                                    <h5><a href="time-line.html" title="">${reply.replyCommentUser.userFirstName} ${reply.replyCommentUser.userLastName}</a></h5>
                                                    <span>${replyFullDate}</span>
                                                </div>
                                                <p>${reply.replyCommentContent}</p>
                                            </div>
                                        </li>`;
                    }).join('') : ''}
                            </ul>
                        </div>
                    </li>`;
                    var replyToArea = document.getElementById(`replyArea${postId}`);
                    if (replyToArea.style.display === "block") {
                        replyToArea.style.display = "none";
                        $(`#parentComment${postId}`).val("");
                    }
                    commentContainer.append(newComment);
                });
                
                // Diğer işlemleri buraya ekleyebilirsiniz, örneğin formu sıfırlamak
            },
            error: function (error) {
                console.log(form.serialize());
                console.error("Yorum gönderme hatası:", error);
                // Hata durumunda kullanıcıya bilgi verilebilir
            }
        });

        return false;
    }



    //inbox page 	
    //***** Message Star *****//  
    $('.message-list > li > span.star-this').on("click", function () {
        $(this).toggleClass('starred');
    });


    //***** Message Important *****//
    $('.message-list > li > span.make-important').on("click", function () {
        $(this).toggleClass('important-done');
    });



    // Listen for click on toggle checkbox
    $('#select_all').on("click", function (event) {
        if (this.checked) {
            // Iterate each checkbox
            $('input:checkbox.select-message').each(function () {
                this.checked = true;
            });
        }
        else {
            $('input:checkbox.select-message').each(function () {
                this.checked = false;
            });
        }
    });


    $(".delete-email").on("click", function () {
        $(".message-list .select-message").each(function () {
            if (this.checked) {
                $(this).parent().slideUp();
            }
        });
    });

    // change background color on hover
    $('.category-box').hover(function () {
        $(this).addClass('selected');
        $(this).parent().siblings().children('.category-box').removeClass('selected');
    });


    //------- offcanvas menu 

    const menu = document.querySelector('#toggle');
    const menuItems = document.querySelector('#overlay');
    const menuContainer = document.querySelector('.menu-container');
    const menuIcon = document.querySelector('.canvas-menu i');

    function toggleMenu(e) {
        menuItems.classList.toggle('open');
        menuContainer.classList.toggle('full-menu');
        menuIcon.classList.toggle('fa-bars');
        menuIcon.classList.add('fa-times');
        e.preventDefault();
    }

    if (menu) {
        menu.addEventListener('click', toggleMenu, false);
    }

    // Responsive nav dropdowns
    $('.offcanvas-menu li.menu-item-has-children > a').on('click', function () {
        $(this).parent().siblings().children('ul').slideUp();
        $(this).parent().siblings().removeClass('active');
        $(this).parent().children('ul').slideToggle();
        $(this).parent().toggleClass('active');
        return false;
    });




});//document ready end





