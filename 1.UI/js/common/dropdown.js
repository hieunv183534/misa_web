
$('.dropdown .dropdown-data').on('click', '.dropdown-item',

    function selectItemDropdown(e) {
        console.log(1);
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
        $(this).parents('.dropdown').find('.dropdown-main p').append(thisValueName);

        if ($(this).parents('.dropdown').attr('id') == 'inputDepartment1Name') {
            filterByDepartment();
        } else if ($(this).parents('.dropdown').attr('id') == 'inputPosition1Name') {
            filterByPosition();
        }
    }

);


