
//tbId -- table Id
//url -- 获取数据的地址,即: actionName
//searchTips  -- search tips
//toolbarId -- toolbar id
function initTable(tbId, url, searchTips, toolbarId) {
    var crnid = "#" + tbId;
    $(crnid).bootstrapTable('destroy').bootstrapTable({
        method: "get",  //使用get请求到服务器获取数据
        url: url, //获取数据的Servlet地址
        striped: true,  //表格显示条纹
        cache: false,
        pagination: true, //启动分页
        paginationHAlign: 'right', //right, left
        paginationVAlign: 'bottom', //bottom, top, both
        paginationDetailHAlign: 'left', //right, left
        pageSize: 15,  //每页显示的记录数
        pageNumber: 1, //当前第几页
        pageList: [15, 20, 25, 30, 35],  //记录数可选列表
        search: true,  //是否启用查询
        toolbar: "#" + toolbarId,
        showColumns: true,  //显示下拉框勾选要显示的列
        showRefresh: true,  //显示刷新按钮
        sidePagination: "server", //server表示服务端请求client
        showPaginationSwitch: false,//展示页数的选择？
        queryParamsType: "limit",
        queryParams: function queryParams(params) {   //设置查询参数
            var param = {
                pageIndex: Math.ceil(params.offset / params.limit) + 1,
                pageSize: params.limit,
                search: params.search,
                sort: params.sort,
                sortOrder: params.order
            };
            return param;
        },
        formatSearch: function () {
            return searchTips;
        },
        onLoadSuccess: function () {  //加载成功时执行
            //layer.msg("加载成功");
        },
        onLoadError: function () {  //加载失败时执行
            //layer.msg("加载数据失败", { time: 1500, icon: 2 });
        }
    });
}

function actionFormatter(value, row, index) {
    return [
     '<a class="mod" href="javascript:void(0)" title="Edit">', '<i class="glyphicon glyphicon-pencil"></i>', '</a>',
     '<a class="delete" href="javascript:void(0)" title="Remove" style="margin-left:10px;">', '<i class="glyphicon glyphicon-remove"></i>', '</a>'
    ].join('');
}


//layer dialog
//openUrl -- 子页面路径
//refreshTbId -- 要刷新的table
//vlidCtenId -- 子页面form Id
function openDialog(openUrl, refreshTbId, vlidFormId, dialogTitle) {
    //var url = '@Html.Raw(Url.Action("Edit"))' + "?id=" + escape(id);
    layer.open({
        title: '<i class="fa fa-plus"></i> ' + dialogTitle,
        type: 2,
        area: ['780px', 'auto'],   //宽高,默认：'auto'
        content: [openUrl, "no"],
        success: function (layero, index) {
            layer.iframeAuto(index)   //iframe自适应高度
        },
        end: function () {    //层销毁后触发的回调
            $("#" + refreshTbId).bootstrapTable('refresh');   //刷新table
        },
        btn: ['Submit', 'Cancel'],
        yes: function (index, layero) {
            //var ifname = "layui-layer-iframe" + index;//获得layer层的名字
            //var Ifame = window.frames[ifname]//得到框架
            var iframeWin = window[layero.find('iframe')[0]['name']]; //得到iframe页的窗口对象，执行iframe页的方法：iframeWin.method();
            //iframeWin.bsvlidtorFormatter();  //execute child method
            //var bv = iframeWin.$("#" + vlidFormId).data('bootstrapValidator').validate();
            layer.iframeAuto(index)   //iframe自适应高度

            if(iframeWin.form_submit())  //调用iframe框中的提交按钮
            {
                $("#" + refreshTbId).bootstrapTable('refresh');   //刷新table
                parent.layer.close(index); //关闭当前的弹窗
            }


            //if (iframeWin.execute_validator()) {
            //    iframeWin.form_submit();
            //    //iframeWin.$("#" + vlidFormId).submit();//调用iframe框中的提交按钮
        
            //}
            //else return;


            //window.parent.location.reload(); //刷新父页面
            //var FormID = eval(Ifame.document.getElementById("mspecContent"))//将字符转成框架中form的对象
        }
    });
}