 
var layerId = request('layerId');
var isPreview = request('isPreview');

var acceptClick;

var auditors = [];
var authorize = [];
var conditions = [];
var workforms = [];

var bootstrap = function ($, learun) {
    "use strict";
    debugger;
    //var currentNode = top[layerId].currentNode;
    var currentNode = top[layerId];
    var formcomponts = {};

    // 加载表单数据
    function loadformcomponts(formId, formName, type) {// 0 添加 1删除
        debugger;
        if (!!formId) {
            if (type == 0) {
                if (!!formcomponts[formId]) {
                    for (i = 0, l = formcomponts[formId].length; i < l; i++) {
                        authorize.push(formcomponts[formId][i]);
                    }
                    $('#authorize_girdtable').jfGridSet('refreshdata', authorize);
                }
                else {
                    formcomponts[formId] = [];
                    $.lrSetForm(top.$.rootUrl + '/LR_FormModule/Custmerform/GetFormData?keyValue=' + formId, function (data) {
                        var scheme = JSON.parse(data.schemeEntity.F_Scheme);
                        for (var i = 0, l = scheme.data.length; i < l; i++) {
                            var componts = scheme.data[i].componts;
                            for (var j = 0, jl = componts.length; j < jl; j++) {
                                var compont = componts[j];
                                if (compont.type == 'gridtable') {
                                    $.each(compont.fieldsData, function (_i, _item) {
                                        if (_item.type != 'guid')
                                        {
                                            var point = { id: learun.newGuid(), formId: formId, formName: formName, fieldName: compont.title + '-' + _item.name, fieldId: compont.id + '|' + _item.id, isLook: '1', isEdit: '1' };
                                            formcomponts[formId].push(point);
                                            authorize.push(point);
                                        }

                                    });
                                }
                                else
                                {
                                    var point = { id: learun.newGuid(), formId: formId, formName: formName, fieldName: compont.title, fieldId: compont.id, isLook: '1', isEdit: '1' };
                                    formcomponts[formId].push(point);
                                    authorize.push(point);
                                }
                            }
                        }
                        $('#authorize_girdtable').jfGridSet('refreshdata', authorize);
                    });
                }
            }
            else {
                var _tmpdata = [];
                for (var i = 0, l = authorize.length; i < l; i++) {
                    if (authorize[i].formId != formId) {
                        _tmpdata.push(authorize[i]);
                    }
                }
                authorize = _tmpdata;
                if (!!currentNode.authorizeFields) {
                    currentNode.authorizeFields = authorize;
                }
                $('#authorize_girdtable').jfGridSet('refreshdata', authorize);
            }
        }
    }

    function isRepeat(id) {
        var res = false;
        for (var i = 0, l = auditors.length; i < l; i++) {
            if (auditors[i].auditorId == id) {
                learun.alert.warning('重复添加审核人员信息');
                res = true;
                break;
            }
        }
        return res;
    }


    var page = {
        init: function () {
            this.nodeInit();
             this.bind();
            this.initData();

            if (!!isPreview) {
                $('input,textarea').attr('readonly', 'readonly');
                $('.lr-form-jfgrid-btns').remove();
            }

        },
        nodeInit: function () {
             

            //switch (currentNode.type) {
            //    case 'startround':// 开始节点
            //        // 去掉审核者设置
            //        $('#lr_form_tabs li a[data-value="auditor"]').parent().remove();
            //        $('#lr_form_tabs li a[data-value="sqlFailInfo"]').parent().remove();
            //        // 超时设置去掉
            //        $('#timeoutNotice').parent().remove();
            //        $('#timeoutAction').parent().remove();
            //        // 去掉会签设置
            //        $('#confluenceType').parent().remove();
            //        $('#confluenceRate').parent().remove();
            //        // 禁止修改节点名称
            //        $('#name').attr('readonly', 'readonly');
            //        break;
            //    case 'auditornode':
            //        $('#lr_form_tabs li a[data-value="sqlFailInfo"]').parent().remove();
            //    case 'stepnode':
            //        // 去掉会签设置
            //        $('#confluenceType').parent().remove();
            //        $('#confluenceRate').parent().remove();
            //        break;
            //    case 'confluencenode':
            //        // 去掉审核者设置
            //        $('#lr_form_tabs li a[data-value="auditor"]').parent().remove();
            //        // 去掉表单权限设置
            //        $('#lr_form_tabs li a[data-value="formAuthorize"]').parent().remove();
            //        $('#lr_form_tabs li a[data-value="workform"]').parent().remove();
            //        // 禁止修改节点名称
            //        $('#name').attr('readonly', 'readonly');
            //        // 超时设置去掉
            //        $('#timeoutNotice').parent().remove();
            //        $('#timeoutAction').parent().remove();
            //        break;
            //    case 'conditionnode':
            //        $('#lr_form_tabs li a[data-value="auditor"]').parent().remove();
            //        $('#lr_form_tabs li a[data-value="workform"]').parent().remove();
            //        $('#lr_form_tabs li a[data-value="formAuthorize"]').parent().remove();
            //        $('#lr_form_tabs li a[data-value="methodInfo"]').parent().remove();
            //        $('#lr_form_tabs li a[data-value="sqlSuccessInfo"]').parent().remove();
            //        $('#lr_form_tabs li a[data-value="sqlFailInfo"]').parent().remove();
            //        // 超时设置去掉
                  
            //        break;
            //};
        },
        /*绑定事件和初始化控件*/
        bind: function () {
            //$('#lr_form_tabs').lrFormTab();
            // 节点设置
            $('#confluenceType').lrselect({
                placeholder: false,
                data: [{ id: '1', text: '会签' }, { id: '2', text: '顺序执行' }]
            }).lrselectSet('1');
          
            console.log("人员添加 方法绑定开始");
            // 人员添加
            $('#lr_user_auditor').on('click', function () {
                debugger;
                learun.layerForm({
                    id: 'AuditorUserForm',
                    title: '添加审核人员',
                    url: '/WF_Scheme/WF_Scheme_UserForm',
                    width: 400,
                    height: 300,
                    callBack: function (id) {
                        debugger;
                        return top[id].acceptClick(function (data) {
                            if (!isRepeat(data.auditorId)) {
                                data.id = learun.newGuid();
                                auditors.push(data);
                                $('#auditor_girdtable').jfGridSet('refreshdata', auditors);
                            }
                        });
                    }
                });
            });
            // 审核人员移除
            $('#lr_delete_auditor').on('click', function () {
                var _id = $('#auditor_girdtable').jfGridValue('id');
                if (learun.checkrow(_id)) {
                    learun.layerConfirm('是否确认删除该审核人员！', function (res,index) {
                        if (res) {
                            for (var i = 0, l = auditors.length; i < l; i++) {
                                if (auditors[i].id == _id) {
                                    auditors.splice(i, 1);
                                    $('#auditor_girdtable').jfGridSet('refreshdata', auditors);
                                    break;
                                }
                            }
                            top.layer.close(index); //再执行关闭  
                        }
                    });
                }
            });
            
        },
        /*初始化数据*/
        initData: function () {
            debugger;
            $('#baseInfo').lrSetFormData(currentNode);
            //$('#iocName').val(currentNode.iocName || '');
            //$('#dbSuccessId').lrselectSet(currentNode.dbSuccessId);
            //$('#dbSuccessSql').val(currentNode.dbSuccessSql || '');
            //$('#dbFailId').lrselectSet(currentNode.dbFailId);
            //$('#dbFailSql').val(currentNode.dbFailSql || '');
            //$('#dbConditionId').lrselectSet(currentNode.dbConditionId);
            //$('#conditionSql').val(currentNode.conditionSql || '');

            //if (!!currentNode.auditors) {
            //    auditors = currentNode.auditors;
            //}
            //if (!!currentNode.authorizeFields) {
            //    authorize = currentNode.authorizeFields;
            //}
            //if (!!currentNode.conditions) {
            //    conditions = currentNode.conditions;
            //}
            //if (!!currentNode.wfForms) {
            //    workforms = currentNode.wfForms;
            //}

            //$('#authorize_girdtable').jfGridSet('refreshdata', authorize);
            //$('#condition_girdtable').jfGridSet('refreshdata', conditions);
            $('#auditor_girdtable').jfGridSet('refreshdata', auditors);
            //$('#workform_girdtable').jfGridSet('refreshdata', workforms);
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#baseInfo').lrValidform()) {
            return false;
        }
        var baseInfoData = $('#baseInfo').lrGetFormData();
        switch (currentNode.type) {
            case 'startround':// 开始节点
                currentNode.authorizeFields = authorize;
                currentNode.wfForms = workforms;
                break;
            case 'auditornode':
            case 'stepnode':
                currentNode.name = baseInfoData.name;
                currentNode.auditors = auditors;
                currentNode.authorizeFields = authorize;
                currentNode.wfForms = workforms;

                currentNode.timeoutAction = baseInfoData.timeoutAction;// 超时流转时间
                currentNode.timeoutNotice = baseInfoData.timeoutNotice;// 超时通知时间
                break;
            case 'confluencenode':
                currentNode.confluenceType = baseInfoData.confluenceType;
                currentNode.confluenceRate = baseInfoData.confluenceRate;
                break;
            case 'conditionnode':
                currentNode.name = baseInfoData.name;
                currentNode.conditions = conditions;
                
                currentNode.dbConditionId = $('#dbConditionId').lrselectGet();
                currentNode.conditionSql = $('#conditionSql').val();
                break;
        };
        if (currentNode.type != 'conditionnode') {
            currentNode.iocName = $('#iocName').val();
            currentNode.dbSuccessId = $('#dbSuccessId').lrselectGet();
            currentNode.dbSuccessSql = $('#dbSuccessSql').val();

            if (currentNode.type != 'startround' && currentNode.type != 'auditornode') {
                currentNode.dbFailId = $('#dbFailId').lrselectGet();
                currentNode.dbFailSql = $('#dbFailSql').val();
            }
        }
        callBack();
        return true;
    };
    console.log("page.init 开始");
    page.init();
    console.log("page.init 结束");
}