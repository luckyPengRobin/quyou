//data validation
function bsvlidtor()
{
    console.log('test');
}

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


//layer dialog
function openDialogtest(id,url) {
    //var url = '@Html.Raw(Url.Action("Edit","VW"))' + "?id=" + escape(id);

    layer.open({
        title: '<i class="fa fa-plus"></i> New Twist Cable',
        type: 2,
        area: ['780px', 'auto'],   //宽高,默认：'auto'
        content: url,
        success: function (layero, index) {
            layer.iframeAuto(index)   //iframe自适应高度

        },
        end: function () {    //层销毁后触发的回调
            $("#tbtstcable").bootstrapTable('refresh');   //刷新table
        },
        btn: ['Submit', 'Cancel'],
        yes: function (index, layero) {
            var ifname = "layui-layer-iframe" + index;//获得layer层的名字
            var Ifame = window.frames[ifname]//得到框架
            Ifame.$('#mspecContent').submit();

            $("#tbtstcable").bootstrapTable('refresh');   //刷新table
            //window.parent.location.reload(); //刷新父页面
            parent.layer.close(index); //关闭当前的弹窗



            //var FormID = eval(Ifame.document.getElementById("teamform1"))//将字符转成框架中form的对象
            //$(Ifame.document.getElementById("teamform1")).submit();
            //Ifame.document.getElementById("btnsubmit").click();     //调用iframe框中的提交按钮
            //Ifame.$("#btnsubmit").click();   //调用iframe框中的提交按钮
        }
    });
}


////添加
//        $("#btnAdd").click(function () {
//            layer.open({
//                title: '<i class="fa fa-plus"></i> New Twist Cable',
//                type: 2,
//                area: ['780px', 'auto'],   //宽高,默认：'auto'
//                content: '@Url.Action("Edit")',
//                success: function (layero, index) {
//                    layer.iframeAuto(index)   //iframe自适应高度

//                },
//                end: function () {    //层销毁后触发的回调
//                    $("#tbtstcable").bootstrapTable('refresh');   //刷新table
//                },
//                btn: ['Submit', 'Cancel'],
//                yes: function (index, layero) {
//                    //按钮【按钮一】的回调
//                    var ifname = "layui-layer-iframe" + index;//获得layer层的名字
//                    var Ifame = window.frames[ifname]//得到框架
//                    Ifame.$("#mspecContent").submit();



//                    //Ifame.$(".form-horizontal").submit();

//                    //$(Ifame.document.getElementById("teamform1")).submit();
//                    //Ifame.document.getElementById("btnsubmit").click();     //调用iframe框中的提交按钮
//                    //Ifame.$("#btnsubmit").click();   //调用iframe框中的提交按钮

//                }
//            , btn2: function (index, layero) {
//                //按钮【按钮二】的回调
//                //return false 开启该代码可禁止点击该按钮关闭
//                return true;
//            }
//            , cancel: function () {
//                //右上角关闭回调
//            }
//            });
//        });


    //window.actionEvents2 = {
    //    'click .mod2': function (e, value, row, index) {
    //        var url = '@Html.Raw(Url.Action("Edit"))' + "?id=" + escape(id);
    //        openDialog(escape(row.Id));
    //    },
    //    'click .delete2': function (e, value, row, index) {
    //        layer.confirm('Are you sure you want to remove？', {
    //            icon: 3,
    //            title: 'Warning',
    //            btn: ['Yes', 'Cancel']
    //        }, function (index) {
    //            $.ajax({
    //                url: '@Url.Content("~/VW/RemoveCable")',
    //                data: { id: row.Id },
    //                success: function (result) {
    //                    if (result.state) {
    //                        //layer.msg(result.msg, { time: 500, icon: 1 });
    //                        $("#tbtstcable").bootstrapTable('refresh');
    //                        layer.close(index);
    //                    }
    //                }
    //            });
    //        }, function () {
    //            //event for Cancel
    //            //layer.msg('Cancelled', { icon: 1 });
    //        });

    //        //alert('You click like action, row: ' + JSON.stringify(row.Id));
    //    }
    //}