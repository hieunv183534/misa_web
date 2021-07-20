$(document).ready(function () {
    new EmployeeJS();
})

/**
 * Class quản lí các sự kiện cho trang employee
 * */
class EmployeeJS extends BaseJS {
    constructor() {
        super();
    }

    setDataUrl() {
        this.dataUrl = "http://cukcuk.manhnv.net/v1/Employees";
    }
    
}


// menu toggle click
$('.toggle.btn-inline').click(function (e) {
    /*e.preventDefault();*/
    if ($('.menu').width() > 50) {
        $('.menu').width(48);
        $('.menu p').hide();
        $('.menu .menu-header .logo').hide();
        $(".content").animate(
            {
                left: "52px",
                width: "100%",
            },
            0,
            () => {
                $(".content").css("width", "calc(100% - 52px)")
            }
        )
    } else {
        $('.menu').width(225);
        setTimeout(function () {
            $('.menu p').show();
            $('.menu .menu-header .logo').show();
        }, 500);      
        $(".content").animate(
            {
                left: "227px",
            },
            0,
            () => {
                $(".content").css("width", "calc(100% - 227px)")
            }
        )
    }
});


// hide show dialog
setTimeout(function () {
    console.log($('.content .content-body .grid table.tb-body tbody tr'))
    $('.content .content-body .grid table.tb-body tbody tr').dblclick(function (e) {
        $('.dialog').css("visibility", "visible");
        $('#autofocus').focus();
    })
}, 3000);
