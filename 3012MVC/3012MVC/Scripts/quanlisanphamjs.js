$(document).ready(function () {
	var sanpham = {
		init: function () {
			sanpham.loadsanpham();
		},
		loadsanpham: function () {
			$.ajax({
				url: '/Sanpham/sanpham',
				type: 'GET',
				dataType: 'json',
				success: function (response) {
					if (response) {
						var data = response.data;
						var html = '';
						var template = $('#teamplatesanpham').html();
						$.each(data, function (i, item) {
							html += Mustache.render(template, {
								MASP: item.MASP,
								TENSP: item.TENSP,
								LOAISP: item.LOAISP,
								HANGSX: item.HANGSX,
								GIA: item.GIA,
								SOLUONG: item.SOLUONG,
								MOTA: item.MOTA,
								ANHDAIDIEN: item.ANHDAIDIEN

							});
						});
						$('#sp').html(html);
					}
				}
			});
		}
	}
	sanpham.init();
})