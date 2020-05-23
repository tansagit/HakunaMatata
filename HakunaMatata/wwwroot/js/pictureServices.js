var pictureServices = {
    init: function () {
        pictureServices.loadPictures();
    },

    loadPictures: function () {

        var id = $('#Id').val();
        $.ajax({
            url: '/AdminArea/Picture/LoadData',
            type: 'GET',
            dataType: 'json',
            data: { realEstateId: id },
            success: function (response) {
                if (response.status) {
                    var data = response.data;
                    var html = '';
                    var template = $('#picture-template').html();
                    $.each(data, function (i, item) {
                        //file local
                        let displayUrl = item.url;
                        if (displayUrl.indexOf('local') === 0) {
                            displayUrl = "/images/" + item.pictureName;
                        }

                        html += Mustache.render(template, {
                            PictureId: item.id,
                            PictureUrl: displayUrl
                        });
                    });

                    $('#view-pictures').html(html);
                }
            }
        });

    },

    removePicture: function (id) {
        if (confirm('Xác nhận xóa hình này?') === true) {

            $.ajax({
                url: '/AdminArea/Picture/Remove',
                type: 'POST',
                dataType: 'json',
                data: { pictureId: id },
                success: function (response) {
                    if (response.status) {
                        pictureServices.loadPictures();

                        setTimeout(function () {
                            alert("Xóa thành công!!!");
                        }, 200);
                    }
                    else {
                        alert("Lỗi xảy ra, vui lòng thử lại!!!");
                    }
                }

            });
        }

    }

};

pictureServices.init();