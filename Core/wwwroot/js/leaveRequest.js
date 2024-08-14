function format(d) {
    // `d` is the original data object for the row
    return (
        '<dl>' +
        '<dt>Leave request content</dt>' +
        '<dd>' +
        d.reason +
        '</dd>' +
        '</dl>'
    );
}

var table = new DataTable('#tblData', {
    ajax: '/admin/leaveRequest/getall',
    columns: [
        {
            className: 'dt-control',
            orderable: false,
            data: null,
            defaultContent: ''
        },
        { data: 'employeeEmail', "width": "20%" },
        {
            data: 'start',
            "render": function (data) {
                let date = new Date(data);
                return date.toDateString();
            },
            "width": "15%"
        },
        {
            data: 'end',
            "render": function (data) {
                let date = new Date(data);
                return date.toDateString();
            },
            "width": "15%"
        },
        {
            data: 'duration',
            "className": "text-end"
        },
        {
            data: 'type',
            "className": "text-end"
        },
        {
            data: 'id',
            "render": function (data) {
                return `<div class="btn-group w-100" role="group">
                     <a href="/admin/leaveRequest/upsert?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>               
                     <a onClick=Delete('/admin/leaveRequest/delete?id=${data}') class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
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
                    table.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    })
}