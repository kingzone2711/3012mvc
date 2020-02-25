$(document).ready(function () {
	var user =
	{
		inits: function () {
			user.registerEvent();
			user.loaddata();

		},
		registerEvent: function () {
			$('#myBtn').off('click').on('click', function () {
				$("#myModal").modal();
			});
			$('#btnSave').off('click').on('click', function () {
				user.savedata();
			});
			$(document).on('click', '.btn-edit', function () {
				$("#myModal").modal();

			});
			$(document).on('click', '.btn-danger', function () {
				var id = $(this).data('id');
				user.delete(id);

			});

		},
		delete: function (id) {
			$.ajax({
				url: '/User/deleteuser',
				type: 'POST',
				data: { id: id },
				dataType: 'json',
				success: function (response) {
					if (response.status == true) {
						alert('xoa thanh cong');
						user.loaddata();
					}

				}
			});
		},
		loaddata: function () {
			$.ajax({
				url: '/User/loaddata',
				type: 'GET',
				dataType: 'json',
				success: function (response) {
					if (response) {
						var data = response.data;
						var html = '';
						var template = $('#data-template').html();
						$.each(data, function (i, item) {
							html += Mustache.render(template, {
								ID: item.ID,
								USERNAME: item.USERNAME,
								NAME: item.NAME,
								ADDRESS: item.ADDRESS,
								EMAIL: item.EMAIL,
								PHONE: item.PHONE,
								SATUS: item.SATUS,
								GROUPID: item.GROUPID

							});
						});
						$('#tbl').html(html);
					}
				}
			});
		},
		savedata: function () {
			var username = $('#txtName').val();
			var id = parseInt($('#hidID').val());
			var status = $('#ckStatus').prop('checked');
			var email = $('#txtemail').val();
			var USSER = {
				USERNAME: username,
				ID: id,
				SATUS: status,
				EMAIL: email
			}
			$.ajax({
				url: '/User/savedata',
				data: {

					struser: JSON.stringify(USSER)
				},
				type: 'POST',
				dataType: 'json',
				success: function (response) {
					if (response.status == true) {

						$("#myModal").modal('hide');
						user.loaddata();

					}
				},
				error: function (err) {
					console.log(err);
				}
			});


		}
	}
	user.inits();
})
