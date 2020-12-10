$("#black").click(function () {
    var s = $("[alt$='间模式']").attr("alt");
    // var s = $(this).html();
    if (s == "夜间模式") {
        $('body').css("background-color", "black") //设置背景颜色为黑色
        $('body').css("opacity", "0.7") //设置不透明度
        $('body').css("-moz-opacity", "0.7")
        $('body').css("filter", "alpha(opacity=50)") //设置滤镜
        $('body').append("<style>::selection {background-color: #d8d5da;color: #fff;}</style>") //设置选中的背景样式
        $('.mb-10,.pb-30,p,li,a,span.lnr').css("color", "white") //给对应字体设置颜色
        $('span.lnr-arrow-up,span.lnr-arrow-down').css("color", "#222") //给箭头设置颜色
        $('.project-area,.info-content,.single-feature,.gallery-area').css("background-color", "black") //给对应模块设置背景样式
        $('.header-btn').css("background-color", "transparent") //设置对应按钮背景颜色透明
        $('.info-content').append("<style>.info-area .info-content::after{background-color: black}</style>") //给对应元素添加样式
        $('.single-feature').append("<style>.p1-gradient-bg, .primary-btn, .primary-btn2:hover, .single-feature:hover, .video-area .overlay-bg, .single-footer-widget .click-btn, .generic-banner {background-image: -webkit-linear-gradient(0deg, #5c5063 0%, #4b4555 100%);}</style>")
        $('.active-works-carousel .owl-dots .owl-dot.active').css("background", "#777777;") //设置背景颜色
        // $(this).html("白天模式");
        // $("[alt='夜间模式']").attr("alt","日间模式");
        $("[alt='夜间模式']").attr({
            "src": "img/sum.png",
            "alt": "日间模式"
        });
    } else {
        $('body').css("background-color", "");  //设置为默认背景颜色
        $('body').css("opacity", "")    //取消不透明
        $('body').css("-moz-opacity", "")
        $('body').css("filter", "") //取消滤镜
        //给对应部件设置样式
        $('body').append("<style>::selection {background-color: #b01afe;color: #fff;}</style>")
        $('.mb-10,.pb-30,p,li,a,span.lnr').css("color", "")
        $('.project-area,.info-content,.single-feature,.gallery-area').css("background-color", "")
        $('.header-btn').css("background-color", "white")
        $('.info-content').append("<style>.info-area .info-content::after{background-color: white}</style>")
        $('.single-feature').append("<style>.p1-gradient-bg, .primary-btn, .primary-btn2:hover, .single-feature:hover, .single-footer-widget .click-btn, .generic-banner {background-image: -moz-linear-gradient(0deg, #b21aff 0%, #732bde 100%);background-image: -webkit-linear-gradient(0deg, #b21aff 0%, #732bde 100%);background-image: -ms-linear-gradient(0deg, #b21aff 0%, #732bde 100%);}</style>")
        // $(this).html("夜间模式");
        $("[alt='日间模式']").attr({
            "src": "img/night.png",
            "alt": "夜间模式"
        });
    }
});

window.onload = function () {
    document.getElementById("right-nav").style.right = "20px";
}