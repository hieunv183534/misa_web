
$('.dropdown .dropdown-data').on('click', '.dropdown-item',

    function selectItemDropdown(e) {
        /*e.preventDefault;*/
        /*$(this).siblings.css('background-color', 'red');*/
        var items = $(this).parents('.dropdown-data').find('.dropdown-item');
        $.each(items, function (index, item) {
            $(item).removeClass('item-selected');
        })

        $(this).addClass('item-selected');
        $(this).parents('.dropdown').find('.dropdown-main p').empty();
        var thisValueId = $(this).attr("valueid");
        var thisValueName = $(this).attr("valuename");
        $(this).parents('.dropdown').attr("value", thisValueId);
        /*$(this).parents('.dropdown').find('.dropdown-main p')*/
        $(this).parents('.dropdown').find('.dropdown-main p').append(thisValueName);
    }

);


