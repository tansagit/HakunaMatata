$(document).ready(function () {
    $('#PhoneNumber').on('focusout', function () {
        var phoneNumber = $(this).val().trim();
        CheckExistedPhoneNumber(phoneNumber);
    });
});

function CheckExistedPhoneNumber(phoneNumber) {
    try {
        if (phoneNumber) {
            $.ajax({
                url: '/AdminArea/Account/CheckExist',
                type: 'POST',
                dataType: 'json',
                data: { phoneNumber: phoneNumber },
                success: function (response) {
                    if (response.isExisted) {
                        $('#check-existed').removeClass('text-navy');
                        $('#check-existed').addClass('text-danger');
                        $('#check-existed').text("Số điện thoại này đã được đăng kí!!!");
                    }
                    else {
                        $('#check-existed').removeClass('text-danger');
                        $('#check-existed').addClass('text-navy');
                        $('#check-existed').text("Bạn có thể sử dụng số điện thoại này!!!");
                    }
                },
                error: function (err) {
                    console.log(err);
                }
            });
        }
    } catch (e) {
        console.log(e);
    }
}


function ConfirmRealEstate(form, flag, isRedirect) {
    if (flag === 1 && confirm('Xác nhận bài viết này?')) {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    if (isRedirect) {
                        window.location.href = "/danh-sach-cho";
                    }
                    else if (res.isSuccess) {
                        $('#confirm-list-all-posts').html(res.html);
                        setTimeout(function () {
                            alert("Xác nhận thành công!");
                        }, 200);
                    }
                    else {
                        alert("Có lỗi xảy ra");
                    }

                },
                error: function (err) {
                    alert("Có lỗi xảy ra! Vui lòng thử lại!");
                    console.log(err);
                }
            });
        } catch (ex) {
            console.log(ex);
        }
    }
    else if (flag === 2 && confirm('Xác nhận từ chối bài viết này?')) {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    if (isRedirect) {
                        window.location.href = "/danh-sach-cho";
                    }
                    else if (res.isSuccess) {
                        $('#confirm-list-all-posts').html(res.html);
                        setTimeout(function () {
                            alert("Xác nhận thành công!");
                        }, 200);
                    }
                    else {
                        alert("Có lỗi xảy ra");
                    }

                },
                error: function (err) {
                    alert("Có lỗi xảy ra! Vui lòng thử lại!");
                    console.log(err);
                }
            });
        } catch (ex) {
            console.log(ex);
        }
    }
    //prevent default form submit event
    return false;
}

function DisableRealEstate(form, isRedirect) {
    if (confirm('Xác nhận khóa bài đăng này?')) {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    if (isRedirect) {
                        window.location.href = "/danh-sach-bai-viet";
                    }
                    else if (res.isSuccess) {
                        $('#user-all-posts').html(res.html);
                    }
                    else {
                        alert("Có lỗi xảy ra");
                    }

                },
                error: function (err) {
                    alert("Có lỗi xảy ra! Vui lòng thử lại!");
                    console.log(err);
                }
            });
        } catch (ex) {
            console.log(ex);
        }
    }

    //prevent default form submit event
    return false;
}

function BookRealEstate(form, isRedirect) {
    if (confirm('Xác nhận phòng này đã được thuê?')) {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    if (isRedirect) {
                        window.location.href = "/danh-sach-bai-viet";
                    }
                    else if (res.isSuccess) {
                        $('#user-all-posts').html(res.html);
                    }
                    else {
                        alert("Có lỗi xảy ra");
                    }

                },
                error: function (err) {
                    alert("Có lỗi xảy ra! Vui lòng thử lại!");
                    console.log(err);
                }
            });
        } catch (ex) {
            console.log(ex);
        }
    }

    //prevent default form submit event
    return false;
}

function UpdateUserInfo(form) {
    try {
        $.ajax({
            type: 'POST',
            url: '/thong-tin-ca-nhan',
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.status) {
                    swal({
                        title: "Thành công!",
                        text: "Cập nhật thông tin thành công!",
                        type: "success"
                    });
                    return;
                }
                else {
                    swal("Oops", "Có lỗi xảy ra, vui lòng thử lại", "error");
                    return;
                }
            },
            error: function (err) {
                swal("Oops", "Có lỗi xảy ra, vui lòng thử lại", "error");
                console.log(err);
            }
        });
    } catch (ex) {
        console.log(ex);
    }

    //prevent default form submit event
    return false;
}
