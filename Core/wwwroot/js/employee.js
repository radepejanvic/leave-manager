var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/employee/getall' },
        "columns": [
            { data: 'email', "width": "20%" },
            { data: 'name', "width": "20%" },
            { data: 'surname', "width": "25%" },
            { data: 'phone', "width": "10%" },
            {
                data: 'email',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                     <a href="/admin/employee/upsert?email=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>               
                     <a onClick=Delete('/admin/employee/delete?email=${data}') class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
                    </div>`
                },
                "width": "25%"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    //toastr.success(data.message);
                }
            })
        }
    })
}