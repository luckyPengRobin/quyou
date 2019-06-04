

//Get Purchase data
function Getpurchs(pnbr) {
    var tableArr = []; //存所有数据
    $("#tbBom tbody tr").each(function () { //便利除标题行外所有行
        var trArr = []; //存行数据
        $("input:not(:button),select,textarea", this).each(function (i) { //便利行内的input select的值

            var tv = $(this).val();
            trArr.push($(this).val());
        });
        console.log(trArr);
        var obj = {};
        if (trArr[0] != '' || trArr[1] != '' || trArr[2] != '' || trArr[3] != '') {
            obj["FBA"] = trArr[0];
            obj["sku"] = trArr[1];
            obj["Qty"] = trArr[2];
            obj["Currency"] = trArr[3];
            obj["UnitPrice"] = trArr[4];
            obj["outerBoxDim"] = trArr[5];
            obj["outerBoxQty"] = trArr[6];
            obj["weight"] = trArr[7];
            obj["destPort"] = trArr[8];
            obj["ShipCat"] = trArr[9];
            obj["PNbr"] = pnbr;
            obj["SupplierId"] = trArr[10];
            obj["Comments"] = trArr[11];


            tableArr.push(obj); //行数据格式
        }

    });
    console.log(tableArr);
    return JSON.stringify(tableArr);

}



//bootstrap fileinput
function BSFileUpload(loadctrId, ChgId, fileType, lstTbId, userId) {
    //var loadId = '#' + loadctrId;
    $('#' + loadctrId).fileinput('refresh', {
        //theme: "explorer-fa", //主题
        uploadUrl: '../Service/Upload', //上传的地址
        uploadExtraData: { "PNbr": ChgId, "filetype": fileType },
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
        preferIconicPreview: false // 是否优先显示图标  false 即优先显示图片

    }).on("filebatchselected", function (event, files) {
        layer.load(1, { shade: [0.5, '#fff'] });  //show loading...
        $(this).fileinput("upload");
    }).on("fileuploaded", function (event, data, previewId, index) {
        $(this).fileinput("clear").fileinput("enable");   //reSet
        if (data.response.state) {
            layer.closeAll('loading');
            listTickets(lstTbId, ChgId, userId, fileType);
        } else {
            layer.msg(data.response.msg, { time: 2000, icon: 2 });
        }

    });


}
//list files,without edit
function listDocs(tbId, RqstId, userId, fileType) {
    var crnid = "#" + tbId;
    $(crnid).bootstrapTable('destroy').bootstrapTable({
        method: "get",  //使用get请求到服务器获取数据
        url: '../Service/ListDocs', //获取数据的Servlet地址
        striped: true,  //表格显示条纹
        cache: false,
        search: false,  //是否启用查询          
        showColumns: false,  //显示下拉框勾选要显示的列
        showRefresh: false,  //显示刷新按钮
        pagination: true, //启动分页
        paginationHAlign: 'right',
        paginationVAlign: 'bottom', //bottom, top, both
        paginationDetailHAlign: 'left', //right, left
        showPaginationSwitch: false,//展示页数的选择？
        pageSize: 10,
        pageNumber: 1, //当前第几页
        pageList: [5, 10, 15, 20, 25],
        sidePagination: "server", //server表示服务端请求client
        queryParamsType: "limit",
        queryParams: function queryParams(params) {   //设置查询参数
            var param = {
                RqstNum: RqstId,
                fileType: fileType,
                userId: userId,
                pageIndex: Math.ceil(params.offset / params.limit) + 1,
                pageSize: params.limit
            };
            return param;
        },
        columns: [{
            field: 'Id',
            title: '付款凭据ID',
            align: 'center',
            visible: false

        }, {
            field: 'FName',
            title: '文件名称',
            align: 'center',
            formatter: function (value, row, index) {
                var sctrl = "<a href='../UploadFiles/" + row.FName + "' target='_blank'>" + row.FName + "</a>";
                return sctrl;
            }
        }, {
            field: 'Ftype',
            title: '凭据类型',
            align: 'center',
            formatter: function (value, row, index) {
                var sctrl = '';
                if (row.Ftype == 'POTicket') {
                    sctrl = '采购支付';
                } else if (row.Ftype == 'TaxTicket') {
                    sctrl = '物流关税支付';
                }
                return sctrl;
            }
        }, {
            field: 'name',
            title: '上传者',
            align: 'center'
        }, {
            field: 'Cdt',
            title: '上传日期',
            align: 'center'
        }],
        onLoadSuccess: function () {  //加载成功时执行
            //layer.msg("加载成功");
        },
        onLoadError: function () {  //加载失败时执行
            //layer.msg("加载数据失败", { time: 1500, icon: 2 });
        }
    });
}


//list files
function listTickets(tbId, RqstId, userId, fileType) {
    var crnid = "#" + tbId;
    $(crnid).bootstrapTable('destroy').bootstrapTable({
        method: "get",  //使用get请求到服务器获取数据
        url: '../Service/ListDocs', //获取数据的Servlet地址
        striped: true,  //表格显示条纹
        cache: false,
        search: false,  //是否启用查询          
        showColumns: false,  //显示下拉框勾选要显示的列
        showRefresh: false,  //显示刷新按钮
        pagination: true, //启动分页
        paginationHAlign: 'right',
        paginationVAlign: 'bottom', //bottom, top, both
        paginationDetailHAlign: 'left', //right, left
        showPaginationSwitch: false,//展示页数的选择？
        pageSize: 10,
        pageNumber: 1, //当前第几页
        pageList: [5, 10, 15, 20, 25],
        sidePagination: "server", //server表示服务端请求client
        queryParamsType: "limit",
        queryParams: function queryParams(params) {   //设置查询参数
            var param = {
                RqstNum: RqstId,
                fileType: fileType,
                userId: userId,
                pageIndex: Math.ceil(params.offset / params.limit) + 1,
                pageSize: params.limit
            };
            return param;
        },
        columns: [{
            field: 'Id',
            title: '付款凭据ID',
            align: 'center',
            visible: false
            
        },
  {
      field: 'FName',
      title: '文件名称',
      align: 'center',
      formatter: function (value, row, index) {
          var sctrl = "<a href='../UploadFiles/" + row.FName + "' target='_blank'>" + row.FName + "</a>";
          return sctrl;
      }
  },
  {
      field: 'Ftype',
      title: '凭据类型',
      align: 'center',
      formatter: function (value, row, index) {
          var sctrl = '';
          if (row.Ftype == 'POTicket') {
              sctrl = '采购支付';
          } else if (row.Ftype == 'TaxTicket') {
              sctrl = '物流关税支付';
          }
          return sctrl;
      }
  }, {
      field: 'Id',
      title: '操作',
      align: 'center',
      formatter: function (value, row, index) {
          //var sctrl = "<a href='javascript:void(0)' onclick='deleteFile(" + row.id + ")' title='Remove'><i class='glyphicon glyphicon-remove'></i></a>";

          var d = '<a href="javascript:void(0)"  onclick="deleteFile(\'' + row.Id + '\',\'' + row.FName + '\',\'' + tbId + '\')" ><span class="glyphicon glyphicon-remove" title="Remove"></span></a> ';
          return d;
      }
  }],
        onLoadSuccess: function () {  //加载成功时执行
            //layer.msg("加载成功");
        },
        onLoadError: function () {  //加载失败时执行
            //layer.msg("加载数据失败", { time: 1500, icon: 2 });
        }
    });
}

//delete attaches
function deleteFile(id, fname, FileTbId) {
    layer.confirm('确定要删除吗？', {
        title: '系统提示',
        //icon: 3,
        btn: ['确定', '取消'], //按钮
        shade: false
    }, function (index) {
        $.ajax({
            url: '../Service/DelFile',
            data: { id: id, fileName: fname },
            success: function (result) {
                if (result.state) {
                    //layer.msg(result.msg, { time: 500, icon: 1 });
                    layer.close(index);
                    $("#" + FileTbId).bootstrapTable('refresh');
                }
                else {
                    layer.msg(result.msg, { time: 1500, icon: 2 });
                }
            }
        });
    });
}


