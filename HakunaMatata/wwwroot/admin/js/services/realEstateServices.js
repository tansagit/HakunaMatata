var pagingOptions = {
    pageSize: 20,
    pageIndex: 1
};

var realEstateServices = {
    init: function () {
        realEstateServices.loadData(true);
        realEstateServices.registerEvent();
    },
    loadData: function (changePageSize) {
        $.ajax({
            url: '/AdminArea/RealEstate/LoadData',
            type: 'GET',
            dataType: 'json',
            data: {
                pageIndex: pagingOptions.pageIndex,
                searchKey: $('#search-for-anything').val()
            },
            success: function (response) {
                var html = '';
                var data = response.data;
                var formData = $('#data-template').html();
                $.each(data, function (i, item) {
                    html += Mustache.render(formData, {
                        Id: item.id,
                        Street: item.street,
                        PostDate: item.postDate,
                        Agent: item.agent,
                        Type: item.type,
                        Status: item.status
                    });
                });

                $('#real-estate-list-data').html(html);
                realEstateServices.paging(response.totalRow, function () {
                    realEstateServices.loadData();
                }, changePageSize);
            },
            error: function (err) {
                console.log(err);
            }
        });
    },

    paging: function (totalRow, callback, changePageSize) {

        var totalPage = Math.ceil(totalRow / pagingOptions.pageSize);

        if ($('#pagination a').length === 0 || changePageSize === true) {
            $('#pagination').empty();
            $('#pagination').removeData("twbs-pagination");
            $('#pagination').unbind("page");
        }

        $('#pagination').twbsPagination({
            totalPages: totalPage,
            //startPage: pagingOptions.pageIndex > totalPage ? totalPage : pagingOptions.pageIndex,
            visiblePages: 5,
            onPageClick: function (event, page) {
                pagingOptions.pageIndex = page;
                setTimeout(callback, 200);
            }
        });
    },
    disableRealEstate: function (form) {
        try {
            if (confirm('Xác nhận khóa phòng trọ này ?')) {
                try {
                    $.ajax({
                        url: '/AdminArea/RealEstate/DisableRealEsate',
                        type: 'POST',
                        headers: {
                            RequestVerificationToken:
                                $('input:hidden[name="__RequestVerificationToken"]').val()
                        },  
                        dataType: 'json',
                        data: new FormData(form),
                        contentType: false,
                        processData: false,
                        success: function (response) {
                            if (response.status) {
                                realEstateServices.loadData(true);
                                setTimeout(function () {
                                    alert("Thành công!");
                                }, 200);
                            }
                            else {
                                alert("Vô hiệu hóa thất bại!");
                            }
                        },
                        error: function (err) {
                            alert(err);
                        }
                    });
                } catch (ex) {
                    console.log(ex);
                }
            }
            //prevent default form submit event
            return false;
        }
        catch (ex) {
            console.log(ex);
        }
    },
    gotoIndex: function () {

    },

    registerEvent: function () {
        $('#search-for-anything').keyup(function () {
            realEstateServices.loadData(true);
        });
    }
};

realEstateServices.init();