$(document).ready(function () {
    $(document).on("click", "#edit-category .photo-ctgr .picture .delete", function () {
        let button = $(this); 
        let id = parseInt(button.attr("data-id"));
        if (button.is(':disabled')) {
            return; 
        }

        $.ajax({
            type: "POST",
            url: `/admin/category/deleteimage?id=${id}`,
            success: function (response) {
                $(`.photo-ctgr[data-id="${id}"]`).remove();
            },
        });
    });

    $(document).on("click", "#edit-category .photo-ctgr .picture .make-main", function () {
        let button = $(this); 
        let id = parseInt(button.attr("data-id"));

        $.ajax({
            type: "POST",
            url: `/admin/category/makemain?id=${id}`,
            success: function (response) {

            },
        });
    });
    $(document).on("click", "#edit-course .photo-ctgr .picture .delete", function () {
        let button = $(this);
        let id = parseInt(button.attr("data-id"));
        if (button.is(':disabled')) {
            return;
        }

        $.ajax({
            type: "POST",
            url: `/admin/course/deleteimage?id=${id}`,
            success: function (response) {
                $(`.photo-ctgr[data-id="${id}"]`).remove();
            },
        });
    });

    $(document).on("click", "#edit-course .photo-ctgr .picture .make-main", function () {
        let button = $(this);
        let id = parseInt(button.attr("data-id"));

        $.ajax({
            type: "POST",
            url: `/admin/course/makemain?id=${id}`,
            success: function (response) {

            },
        });
    });
});

