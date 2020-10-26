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
				var id = $(this).data('id');
				user.Loadetail(id);

			});
			$(document).on('click', '.btn-danger', function () {
				if (confirm("Xác nhận xóa") == true) {
					var id = $(this).data('id');
					user.delete(id);
				}
			});

		},
		delete: function (id) {
			$.ajax({
				url: '/Admin/User/deleteuser',
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
				url: '/Admin/User/loaddata',
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
				url: '/Admin/User/savedata',
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


		},
		Loadetail: function (id)
		{
			$.ajax({
				url: '/Admin/User/loaddetai',
				data: {
					id: id
				},
				type: 'GET',
				dataType: 'json',
				success: function (response) {
					if (response.status == true) {
						var data = response.data;
						$('#hidID').val(data.ID);
						$('#txtName').val(data.NAME);
						$('#txtAddress').val(data.EMAIL);
						$('#ckStatus').prop('checked', data.SATUS);
					}
					else {
						bootbox.alert(response.message);
					}
				},
				error: function (err) {
					console.log(err);
				}
			});
		},

	}
	user.inits();
})
