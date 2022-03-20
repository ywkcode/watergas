 
var acceptClick;
var auditorName = '';
var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            this.bind();
        },
        bind: function () {
            debugger;
            $('#auditorId').lrselect({
                value: 'f_UserId',
                text: 'f_RealName',
                title: 'f_RealName',
                // 展开最大高度
                maxHeight: 110,
                // 是否允许搜索
                allowSearch: true,
                select: function (item) {
                    auditorName = item.f_RealName;
                }
            });
            $('#department').lrDepartmentSelect({
                maxHeight: 150,
                companyId: "",
                parentId: "",
            }).on('change', function () {
                debugger
                var value = $(this).lrselectGet();
                $('#auditorId').lrselectRefresh({
                    url: '/FrameUser/GetTree',
                    param: { departmentId: value }
                });
            });

            
          
          
           
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        
        var formData = $('#form').lrGetFormData();
        formData.auditorName = auditorName;
        formData.type = '3';//审核者类型1.岗位2.角色3.用户
        callBack(formData);
        //return true;
    };
    page.init();
}