function format(d) {
    // `d` is the original data object for the row
    return (
        '<dl>' +
        '<dt>Vacation Days:</dt>' +
        '<dd>' +
        d.totalVacationDays +
        '</dd>' +
        '<dt>Remote Days:</dt>' +
        '<dd>' +
        d.totalRemoteDays +
        '</dd>' +
        '<dt>Sick Days:</dt>' +
        '<dd>' +
        d.totalSickDays +
        '</dd>' +
        '</dl>'
    );
}

let table = new DataTable('#tblData', {
    ajax: '/admin/employee/getall',
    columns: [
        {
            className: 'dt-control',
            orderable: false,
            data: null,
            defaultContent: ''
        },
        { data: 'employee.email', "width": "20%" },
        { data: 'employee.name', "width": "20%" },
        { data: 'employee.surname', "width": "25%" },
        { data: 'employee.phone', "width": "10%" },
        {
            data: 'employee.email',
            "render": function (data) {
                return `<div class="btn-group w-100" role="group">
                     <a href="/admin/employee/upsert?email=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>
                     <a onClick=Delete('/admin/employee/delete?email=${data}') class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
                    </div>`
            },
            "width": "25%"
        }
    ],
    order: [[1, 'asc']]
});

table.on('click', 'td.dt-control', function (e) {
    let tr = e.target.closest('tr');
    let row = table.row(tr);

    if (row.child.isShown()) {
        row.child.hide();
    }
    else {
        row.child(format(row.data())).show();
    }
});

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
                    toastr.success(data.message);
                }
            })
        }
    })
}