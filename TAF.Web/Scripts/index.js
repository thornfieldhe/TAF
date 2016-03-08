$(document).ready(function () {

    $(".sidebar-menu a").click(function (e) {
        $(".sidebar-menu .active").each(function (r) {
            $(this).removeClass("active");
        });
        $(this).parent().addClass("active");
    });

    function loadPage(url, parentTitle, title, document, isHomePage) {


        $.get(url, function (data) {

            $(".page-body").html(data);
            if (!isHomePage) {
                $(".breadcrumb").html("<li><i class='fa fa-home'></i><a href='#/home' >主页</a></li><li >" + parentTitle + "</li><li >" + title + "</li>");
            } else {
                $(".breadcrumb").html("<li><i class='fa fa-home'></i><a href='#/home' >主页</a></li>");
            }
            $(".sidebar-menu .active").each(function (r) {
                $(this).parent().parent().removeClass("open");
                $(this).removeClass("active");
            });
            $(document).parent().parent().addClass("open");
            $(document).addClass("active");
            $("#bodyTitle").html(parentTitle);
            
        });
    }

//    function LoadResources() {
//        $.getJSON("/Home/GetResources", function (data) {
//            taf.resources = data;
//            console.log(data);
//            console.log(taf.resources);
//        });
//    }

    //加载路由
    var homePage = function () {
        loadPage("/Home/Dashboard", "主页", "主页", "#menuHome", true);
    }
    var changePwPage = function () {
        loadPage("/Home/ChangePasswordIndex", "用户管理", "修改密码", "#menuChangePass", false);
    }
    var usersPage = function () {
        loadPage("/Home/UserIndex", "用户管理", "用户列表", "#menuUsers", false);
    }
    var dictionaryPage = function () {
        loadPage("/Dictionary/Index", "商品管理", "字典列表", "#menuDictionary", false);
    }
    var productPage = function () {
        loadPage("/Product/Index", "商品管理", "商品列表", "#menuProduct", false);
    }
    var routes = {
        '/': homePage,
        '/home': homePage,
        '/users': usersPage,
        '/changePw': changePwPage,
        '/dictionary': dictionaryPage,
        '/product': productPage
    };
    var router = Router(routes);
    router.init();
});



