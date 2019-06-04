//select2 for SKU
function SKUSearch(crnId) {
    $("#" + crnId).select2({
        ajax: {
            cache: false,
            dataType: "json",
            type: "GET",
            url: '../Service/List_SKUs',
            delay: 250,
            success: function (data) {
            },
            data: function (params) {
                return {
                    query: params.term   // 查询的关键字
                };
            },
            processResults: function (data) {
                return {
                    results: data
                };
            }
        },
        placeholder: '请选择..',   //需要select控件使用默认,或者placeholder无效
        allowClear: true,
        theme: 'classic',
        minimumInputLength: 1,
        //minimumResultsForSearch: 5, // at least 20 results must be displayed
        //maximumSelectionLength: 3, //最多能够选择的个数
        tags: false,//允许手动添加
        templateResult: function (res) { return res.text; },    // 显示查询结果
        templateSelection: function (res) { return res.text; },    // 显示选中的对象
        escapeMarkup: function (markup) { return markup; }
    });
}



//select2 for Port
function PortSearch(crnId) {
    $("#" + crnId).select2({
        ajax: {
            cache: false,
            dataType: "json",
            type: "GET",
            url: '../Service/List_Port',
            delay: 250,
            success: function (data) {
            },
            data: function (params) {
                return {
                    query: params.term   // 查询的关键字
                };
            },
            processResults: function (data) {
                return {
                    results: data
                };
            }
        },
        placeholder: '请选择..',   //需要select控件使用默认,或者placeholder无效
        allowClear: true,
        theme: 'classic',
        minimumInputLength: 1,
        //minimumResultsForSearch: 5, // at least 20 results must be displayed
        //maximumSelectionLength: 3, //最多能够选择的个数
        tags: false,//允许手动添加
        templateResult: function (res) { return res.text; },    // 显示查询结果
        templateSelection: function (res) { return res.text; },    // 显示选中的对象
        escapeMarkup: function (markup) { return markup; }
    });
}


//select2 for Supplier
function SupplierSearch(crnId) {
    $("#" + crnId).select2({
        ajax: {
            cache: false,
            dataType: "json",
            type: "GET",
            url: '../Service/list_Supplier',
            delay: 250,
            success: function (data) {
            },
            data: function (params) {
                return {
                    query: params.term   // 查询的关键字
                };
            },
            processResults: function (data) {
                return {
                    results: data
                };
            }
        },
        placeholder: '请选择..',   //需要select控件使用默认,或者placeholder无效
        allowClear: true,
        theme: 'classic',
        minimumInputLength: 1,
        //minimumResultsForSearch: 5, // at least 20 results must be displayed
        //maximumSelectionLength: 3, //最多能够选择的个数
        tags: false,//允许手动添加
        templateResult: function (res) { return res.text; },    // 显示查询结果
        templateSelection: function (res) { return res.text; },    // 显示选中的对象
        escapeMarkup: function (markup) { return markup; }
    });
}
//select2 for people search,return <userId,Name>
function peopleSearchNmaeId(crnId) {
    $("#" + crnId).select2({
        ajax: {
            cache: false,
            dataType: "json",
            type: "GET",
            url: '../Service/ListUsers',
            delay: 250,
            success: function (data) {
            },
            data: function (params) {
                return {
                    query: params.term   // 查询的关键字
                };
            },
            processResults: function (data) {
                return {
                    results: data
                };
            }
        },
        placeholder: 'Select a person...',   //需要select控件使用默认默认,或者placeholder无效
        allowClear: true,
        theme: 'classic',
        minimumInputLength: 2,
        //minimumResultsForSearch: 5, // at least 20 results must be displayed   myteammodal
        maximumSelectionLength: 3, //最多能够选择的个数
        tags: false,//允许手动添加
        templateResult: function (obj) { return obj.text; },    // 显示查询结果
        templateSelection: function (user) { return user.text; },    // 显示选中的对象
        escapeMarkup: function (markup) { return markup; }
    });
}


//select2 for people search
function peopleSearch(crnId) {
    $("#" + crnId).select2({
        ajax: {
            cache: false,
            dataType: "json",
            type: "GET",
            url: '../Service/ListUsers',
            delay: 250,
            success: function (data) {
            },
            data: function (params) {
                return {
                    query: params.term   // 查询的关键字
                };
            },
            processResults: function (data) {
                return {
                    results: data
                };
            }
        },
        placeholder: 'Select a person...',   //需要select控件使用默认默认,或者placeholder无效
        allowClear: true,
        theme: 'classic',
        minimumInputLength: 2,
        //minimumResultsForSearch: 5, // at least 20 results must be displayed   myteammodal
        maximumSelectionLength: 3, //最多能够选择的个数
        tags: false,//允许手动添加
        templateResult: function (obj) { return obj.text; },    // 显示查询结果
        templateSelection: function (user) { return user.text; },    // 显示选中的对象
        escapeMarkup: function (markup) { return markup; }
    });
}

function ListPEUsers(clsnm) {
    $(clsnm).select2({
        ajax: {
            cache: false,
            dataType: "json",
            type: "GET",
            url: '../Service/List_PE_Users',
            delay: 250,
            success: function (data) {
            },
            data: function (params) {
                return {
                    query: params.term   // 查询的关键字
                };
            },
            processResults: function (data) {
                return {
                    results: data
                };
            }
        },
        placeholder: 'Select a person...',   //需要select控件使用默认默认,或者placeholder无效
        allowClear: true,
        theme: 'classic',
        minimumInputLength: 2,
        //minimumResultsForSearch: 5, // at least 20 results must be displayed   myteammodal
        maximumSelectionLength: 3, //最多能够选择的个数
        tags: false,//允许手动添加
        templateResult: function (obj) { return obj.text; },    // 显示查询结果
        templateSelection: function (user) { return user.text; },    // 显示选中的对象
        escapeMarkup: function (markup) { return markup; }
    });
}







//select2 for customer
function customerSearch(crnId) {
    $("#" + crnId).select2({
        ajax: {
            cache: false,
            dataType: "json",
            type: "GET",
            url: '../Service/List_Customers',
            delay: 250,
            success: function (data) {
            },
            data: function (params) {
                return {
                    query: params.term   // 查询的关键字
                };
            },
            processResults: function (data) {
                return {
                    results: data
                };
            }
        },
        //placeholder: 'Select a customer...',   //需要select控件使用默认,或者placeholder无效
        allowClear: true,
        theme: 'classic',
        minimumInputLength: 1,
        //minimumResultsForSearch: 5, // at least 20 results must be displayed
        //maximumSelectionLength: 3, //最多能够选择的个数
        tags: false,//允许手动添加
        templateResult: function (res) { return res.text; },    // 显示查询结果
        templateSelection: function (res) { return res.text; },    // 显示选中的对象
        escapeMarkup: function (markup) { return markup; }
    });
}






//select2 for remote data retrieve  -- Opp Number
function OppNumSearch(crnId) {
    $("#" + crnId).select2({
        ajax: {
            cache: false,
            dataType: "json",
            type: "GET",
            url: '../Service/List_OppNo',
            delay: 250,
            success: function (data) {
            },
            data: function (params) {
                return {
                    query: params.term   // 查询的关键字
                };
            },
            processResults: function (data) {
                return {
                    results: data
                };
            }
        },
        placeholder: 'Select One...',   //需要select控件使用默认,或者placeholder无效
        allowClear: true,
        theme: 'classic',
        minimumInputLength: 1,
        //minimumResultsForSearch: 5, // at least 20 results must be displayed
        //maximumSelectionLength: 3, //最多能够选择的个数
        tags: false,//允许手动添加
        templateResult: function (res) { return res.text; },    // 显示查询结果
        templateSelection: function (res) { return res.text; },    // 显示选中的对象
        escapeMarkup: function (markup) { return markup; }
    });
}



function dateTimePicker(crnId) {
    $('#' + crnId).datetimepicker({
        autoclose: true,
        clearBtn: 0,
        //todayBtn: true,
        format: "yyyy-mm-dd",
        startView: 2,
        minView: 2,
        startDate: new Date()
    });

}


//bootstrap fileinput
function BSFileUpload(loadctrId, ChgId,lstTbId) {
    //var loadId = '#' + loadctrId;
    $('#' + loadctrId).fileinput('refresh', {
        //theme: "explorer-fa", //主题       
        uploadUrl: '../Service/Upload', //上传的地址
        uploadExtraData: { "ChgId": ChgId },
        //allowedFileExtensions: ['xls', 'xlsx'],//接收的文件后缀
        uploadAsync: true, //默认异步上传
        showUpload: true, //是否显示上传按钮
        showRemove: false, //显示移除按钮
        showPreview: true, //是否显示预览
        showCaption: true,//是否显示标题
        browseClass: "btn btn-primary btn-sm", //按钮样式    
        autoReplace: true,
        showCancel: false,
        dropZoneEnabled: false,//是否显示拖拽区域
        removeFromPreviewOnError: false,//是否移除校验文件失败的文件  
        maxFileCount: 5, //表示允许同时上传的最大文件个数
        enctype: 'multipart/form-data',
        validateInitialCount: true,
        layoutTemplates: {
            actionUpload: '',   //取消上传按钮 
            actionZoom: ''      //取消放大镜按钮 
        },
        maxFileSize: 1024 * 10,  //允许文件上传大小  
        overwriteInitial: false,
        previewFileIcon: '<i class="fa fa-file"></i>',
        initialPreviewAsData: true, // defaults markup    
        preferIconicPreview: true, // 是否优先显示图标  false 即优先显示图片  
        previewFileIconSettings: { // configure your icon file extensions   
            'doc': '<i class="fa fa-file-word-o text-primary"></i>',
            'docx': '<i class="fa fa-file-word-o text-primary"></i>',
            'xls': '<i class="fa fa-file-excel-o text-success"></i>',
            'xlsx': '<i class="fa fa-file-excel-o text-success"></i>',
            'ppt': '<i class="fa fa-file-powerpoint-o text-danger"></i>',
            'pdf': '<i class="fa fa-file-pdf-o text-danger"></i>',
            'zip': '<i class="fa fa-file-archive-o text-muted"></i>',
            'htm': '<i class="fa fa-file-code-o text-info"></i>',
            'txt': '<i class="fa fa-file-text-o text-info"></i>',
            'mov': '<i class="fa fa-file-movie-o text-warning"></i>',
            'mp3': '<i class="fa fa-file-audio-o text-warning"></i>',
            'jpg': '<i class="fa fa-file-photo-o text-danger"></i>',
            'gif': '<i class="fa fa-file-photo-o text-muted"></i>',
            'png': '<i class="fa fa-file-photo-o text-primary"></i>'
        }
    }).on("fileuploaded", function (event, data, previewId, index) {       
        $(this).fileinput("clear").fileinput("enable");   //reSet
        if (data.response.state) {
            ListFiles2Table(lstTbId, ChgId);
        } else {
            layer.msg(data.response.msg, { time: 2000, icon: 2 });
        }

    });

    
}


//list files
function ListFiles2Table(tbId, RqstId) {
    var crnid = "#" + tbId;
    $(crnid).bootstrapTable('destroy').bootstrapTable({
        method: "get",  //使用get请求到服务器获取数据
        url: '../Service/ListDocs', //获取数据的Servlet地址
        striped: true,  //表格显示条纹
        cache: false,
        pagination: false, //启动分页            
        search: false,  //是否启用查询          
        showColumns: false,  //显示下拉框勾选要显示的列
        showRefresh: false,  //显示刷新按钮
        sidePagination: "server", //server表示服务端请求client
        showPaginationSwitch: false,//展示页数的选择？
        queryParamsType: "limit",
        queryParams: function queryParams(params) {   //设置查询参数
            var param = {
                RqstNum: RqstId,
            };
            return param;
        },
        onLoadSuccess: function () {  //加载成功时执行
            //layer.msg("加载成功");
        },
        onLoadError: function () {  //加载失败时执行
            //layer.msg("加载数据失败", { time: 1500, icon: 2 });
        }
    });
}




