var PersonController = function () {
    this.initialize = function () {
        loadData();

    };

    function loadData() {
        $.ajax({
            type: 'GET',
            url: '/admin/person/GetAll',
            dataType: 'json',
            success: function (response) {
                console.log(response);
                $('#datatable-responsives').DataTable({
                    data: response,
                    'scrollY': 400,
                    columns: [
                        {
                            'data': 'Id',
                            'render': function (Id) {
                                return '<a class="btn btn-sm btn-edit" onclick="eventEditPerson(\'' + Id + '\')"><i class="fa fa-pencil"></i></a>' +
                                '<a id="btnDelete" class="btn btn-sm btn-delete" onclick="eventDeletePerson(\'' + Id + '\')"><i class="fa fa-trash"></i></a>'
                            }
                        },
                        {'data': 'Name'},
                        {'data': 'Code'},
                        {
                            'data': 'DateOfBirth',
                            'render': function (datetime) {
                                if (datetime === null || datetime === '')
                                    return '';
                                var newdate = new Date(datetime);
                                var month = newdate.getMonth() + 1;
                                var day = newdate.getDate();
                                var year = newdate.getFullYear();
                                var hh = newdate.getHours();
                                var mm = newdate.getMinutes();
                                var ss = newdate.getSeconds();
                                if (month < 10)
                                    month = "0" + month;
                                if (day < 10)
                                    day = "0" + day;
                                if (hh < 10)
                                    hh = "0" + hh;
                                if (mm < 10)
                                    mm = "0" + mm;
                                if (ss < 10)
                                    ss = "0" + ss;
                                return day + "/" + month + "/" + year + " " + hh + ":" + mm + ":" + ss;
                            }

                        },
                        {'data': 'Job'},
                        {
                            'data': 'CreatedDate',
                            'render': function (datetime) {
                                if (datetime === null || datetime === '')
                                    return '';
                                var newdate = new Date(datetime);
                                var month = newdate.getMonth() + 1;
                                var day = newdate.getDate();
                                var year = newdate.getFullYear();
                                var hh = newdate.getHours();
                                var mm = newdate.getMinutes();
                                var ss = newdate.getSeconds();
                                if (month < 10)
                                    month = "0" + month;
                                if (day < 10)
                                    day = "0" + day;
                                if (hh < 10)
                                    hh = "0" + hh;
                                if (mm < 10)
                                    mm = "0" + mm;
                                if (ss < 10)
                                    ss = "0" + ss;
                                return day + "/" + month + "/" + year + " " + hh + ":" + mm + ":" + ss;
                            }
                        },
                        {
                            'data': 'ModifiedDate',
                            'render': function (datetime) {
                                if (datetime === null || datetime === '')
                                    return '';
                                var newdate = new Date(datetime);
                                var month = newdate.getMonth() + 1;
                                var day = newdate.getDate();
                                var year = newdate.getFullYear();
                                var hh = newdate.getHours();
                                var mm = newdate.getMinutes();
                                var ss = newdate.getSeconds();
                                if (month < 10)
                                    month = "0" + month;
                                if (day < 10)
                                    day = "0" + day;
                                if (hh < 10)
                                    hh = "0" + hh;
                                if (mm < 10)
                                    mm = "0" + mm;
                                if (ss < 10)
                                    ss = "0" + ss;
                                return day + "/" + month + "/" + year + " " + hh + ":" + mm + ":" + ss;
                            }
                        },
                        {
                            'data': 'Avatar',
                            'render': function (avatar) {
                                if (avatar == null) {
                                    return '<img src="/admin-side/images/user.png" width=25';
                                } else {
                                    return '<img src="' + avatar + '" width=25 />'
                                }
                            }
                        },
                        {
                            'data': 'Status',
                            'render': function (status) {
                                if (status === 0)
                                    return '<span class="badge bg-green">Active</span>';
                                else
                                    return '<span class="badge bg-red">Locked</span>';

                            }
                        }
                    ]
                });
                util.notify("Completed load data", 'success')
            },
            error: function (status) {
                console.log("Error: ", status);
                util.notify("Cannot loading data", error)
            }
        });
    }
};

function eventEditPerson(Id) {
     $('#modal-add-edit').modal('show');
    console.log('edit ' + Id);
}