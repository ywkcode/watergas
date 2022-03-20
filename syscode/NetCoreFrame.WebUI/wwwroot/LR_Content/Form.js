 
var keyValue = "";
var categoryId = "";
var shcemeCode = request('shcemeCode');

var currentNode; // 当前设置节点
var currentLine; // 当前设置线条
var schemeAuthorizes = []; // 模板权限人员
var authorizeType = 1;// 模板权限类型

var formtype = request('formtype');

var bootstrap = function ($, learun) {
    "use strict";

    function isRepeat(id) {
        var res = false;
        for (var i = 0, l = schemeAuthorizes.length; i < l; i++) {
            if (schemeAuthorizes[i].F_ObjectId == id) {
                learun.alert.warning('重复添加信息');
                res = true;
                break;
            }
        }
        return res;
    }

    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        /*绑定事件和初始化控件*/
        bind: function () {
         
            // 设计页面初始化
            $('#step-3').lrworkflow({
                openNode: function (node) {
                    debugger;
                    currentNode = node;
                    top["layer_Form"] = currentNode;
                    
                    if (node.type != 'endround') {
                        
                        learun.layerForm({
                            id: 'NodeForm',
                            title: '节点信息设置【' + node.name + '】',
                            url: '/WF_Scheme/WF_Scheme_NodeForm?layerId=layer_Form',
                            width: 1000,
                            height: 400,
                            callBack: function (id) {
                                return top[id].acceptClick(function () {
                                    $('#step-3').lrworkflowSet('updateNodeName', { nodeId: currentNode.id });
                                });
                            }
                        });
                        //layer.open
                        //    ({
                        //        type: 2,
                        //        title: '节点信息设置【' + node.name + '】',
                        //        content: '/WF_Scheme/WF_Scheme_NodeForm?layerId=layer_Form',
                        //        area: ['700px', '500px'],
                        //        btnAlign: 'c',
                        //        end: function () {
                        //            回调业务
                        //            location.reload();
                        //        }
                        //    });
                    }
                },
                openLine: function (line) {
                    currentLine = line;
                    learun.layerForm({
                        id: 'LineForm',
                        title: '线条信息设置',
                        url: top.$.rootUrl + '/LR_WorkFlowModule/WfScheme/LineForm?layerId=layer_Form',
                        width: 400,
                        height: 300,
                        callBack: function (id) {
                            return top[id].acceptClick(function () {
                                $('#step-3').lrworkflowSet('updateLineName', { lineId: currentLine.id });
                            });
                        }
                    });
                }
            });
            // 保存草稿
            $("#btn_draft").on('click', page.draftsave);
            // 保存数据按钮
            $("#btn_finish").on('click', page.save);
        },
        /*初始化数据*/
        initData: function () {
            debugger;
            if (!!shcemeCode) {
                $.lrSetForm('/WF_Scheme/GetFormData?schemeCode=' + shcemeCode, function (data) {//
                    
                    
                    var shceme = JSON.parse(data.result);
                    $('#step-3').lrworkflowSet('set', { data: shceme });

                    //if (data.wfSchemeAuthorizeList.length > 0 && data.wfSchemeAuthorizeList[0].F_ObjectType != 4) {
                    //    $('#authorizeType2').trigger('click');
                    //    schemeAuthorizes = data.wfSchemeAuthorizeList;
                    //    $('#authorize_girdtable').jfGridSet('refreshdata', { rowdatas: schemeAuthorizes });
                    //    authorizeType = 2;
                    //}
                });
            }
        },
        /*保存草稿*/
        draftsave: function () {
            debugger;
            var formdata = $('#step-1').lrGetFormData(keyValue);
            var shcemeData = $('#step-3').lrworkflowGet();

            if (authorizeType == 1) {
                schemeAuthorizes = [];
            }

            var postData = {
                schemeInfo: JSON.stringify(formdata),
                scheme: JSON.stringify(shcemeData),
                shcemeAuthorize: JSON.stringify(schemeAuthorizes),
                type: 2
            };

            $.lrSaveForm(top.$.rootUrl + '/LR_WorkFlowModule/WfScheme/SaveForm?keyValue=' + keyValue, postData, function (res) {
                // 保存成功后才回调
                learun.frameTab.currentIframe().refreshGirdData(formdata);
            });
        },
        /*保存数据*/
        save: function () {
            debugger;
            //if (!$('#step-1').lrValidform()) {
            //    return false;
            //}
            var formdata = $('#step-1').lrGetFormData(keyValue);
            var shcemeData = $('#step-3').lrworkflowGet();

            if (authorizeType == 1) {
                schemeAuthorizes = [];
                schemeAuthorizes.push({
                    F_Id: learun.newGuid(),
                    F_ObjectType: 4
                });
            }
            var postData = {
                schemeInfo: JSON.stringify(formdata),
                scheme: JSON.stringify(shcemeData),
                shcemeCode: shcemeCode,
                type: formtype
            };
            $.lrSaveForm('/WF_Scheme/SaveForm?keyValue=' + keyValue, postData, function (res) {
                // 保存成功后才回调

                var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引 
                parent.layer.close(index); //再执行关闭

            });

            
        }
    };

    page.init();
}