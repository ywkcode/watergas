(global["webpackJsonp"]=global["webpackJsonp"]||[]).push([["pages/center/index"],{"22b8":function(e,t,a){"use strict";a.r(t);var n=a("9704"),r=a("c4ac");for(var i in r)"default"!==i&&function(e){a.d(t,e,(function(){return r[e]}))}(i);a("82f3");var s,o=a("f0c5"),u=Object(o["a"])(r["default"],n["b"],n["c"],!1,null,"6eeabd49",null,!1,n["a"],s);t["default"]=u.exports},"3ac9":function(e,t,a){},"82f3":function(e,t,a){"use strict";var n=a("3ac9"),r=a.n(n);r.a},"8b86":function(e,t,a){"use strict";(function(e){a("8c7d");n(a("66fd"));var t=n(a("22b8"));function n(e){return e&&e.__esModule?e:{default:e}}wx.__webpack_require_UNI_MP_PLUGIN__=a,e(t.default)}).call(this,a("543d")["createPage"])},9704:function(e,t,a){"use strict";a.d(t,"b",(function(){return r})),a.d(t,"c",(function(){return i})),a.d(t,"a",(function(){return n}));var n={uSubsection:function(){return a.e("uview-ui/components/u-subsection/u-subsection").then(a.bind(null,"c9ad"))},myarcbar:function(){return a.e("components/myarcbar/myarcbar").then(a.bind(null,"e819"))},uTag:function(){return a.e("uview-ui/components/u-tag/u-tag").then(a.bind(null,"4ed4"))},uGap:function(){return a.e("uview-ui/components/u-gap/u-gap").then(a.bind(null,"668e"))},myarea:function(){return a.e("components/myarea/myarea").then(a.bind(null,"3778"))},mytable:function(){return a.e("components/mytable/mytable").then(a.bind(null,"2e70"))},mydefinetable:function(){return a.e("components/mydefinetable/mydefinetable").then(a.bind(null,"8164"))}},r=function(){var e=this,t=e.$createElement;e._self._c},i=[]},c4ac:function(e,t,a){"use strict";a.r(t);var n=a("d4b9"),r=a.n(n);for(var i in n)"default"!==i&&function(e){a.d(t,e,(function(){return n[e]}))}(i);t["default"]=r.a},d4b9:function(e,t,a){"use strict";(function(e){Object.defineProperty(t,"__esModule",{value:!0}),t.default=void 0;var n=r(a("a34a"));function r(e){return e&&e.__esModule?e:{default:e}}function i(e,t,a,n,r,i,s){try{var o=e[i](s),u=o.value}catch(c){return void a(c)}o.done?t(u):Promise.resolve(u).then(n,r)}function s(e){return function(){var t=this,a=arguments;return new Promise((function(n,r){var s=e.apply(t,a);function o(e){i(s,n,r,o,u,"next",e)}function u(e){i(s,n,r,o,u,"throw",e)}o(void 0)}))}}var o={data:function(){return{title:"Hello",chartData:{categories:[],series:{name:"TOC",data:[]}},mytestdata:{title:"测试1"},opts:{},list:["实时数据","历史数据","异常数据"],current:0,firstTab:!0,secondTab:!1,thirdTab:!1,isgas:!1,seriesname:"TOC",seriesdata:.7,titlename:"03mh",mytablist:[],variableNames:{ad:"0.000",cL2:"0.000",h2S:"0.000",hcl:"0.000",ll:"0.000",nH3:"0.000",ph:"0.000",toc:"0.000",zl:"0.000"},chooseData:{variableName:"TOC",chosenData:"10",chosenPercent:.5},myTableDatas:[],historyData:{namelists:{},historyTableResponses:[]},errorData:{namelists:{},historyTableResponses:[]}}},onLoad:function(e){this.isgas="1"==e.isgas,1==this.isgas&&(this.seriesname="H2S"),0==this.isgas&&(this.seriesname="TOC"),this.loadfive(this.seriesname)},onBackPress:function(t){return e.navigateTo({url:"/pages/index/index"}),!0},onReady:function(){this.mytestdata={title:"ceshi"}},methods:{click:function(e){console.log("item",e)},sectionChange:function(e){this.firstTab=!1,this.secondTab=!1,this.thirdTab=!1,0==e&&(this.firstTab=!0,loadfive(this.seriesname)),1==e&&(this.secondTab=!0,this.loadhistory()),2==e&&(this.thirdTab=!0,this.loaderror())},refreshArcbar:function(){var e=this;setTimeout((function(){e.$refs.myarcbar.getServerData()}),100)},refreashArea:function(){var e=this;setTimeout((function(){e.$refs.myarea.getServerData()}),100)},loadfive:function(e){var t=this;return s(n.default.mark((function a(){var r,i;return n.default.wrap((function(a){while(1)switch(a.prev=a.next){case 0:return r={VariableName:e,IsGas:t.isgas},a.next=3,t.$u.api.loadfiveindex(r);case 3:i=a.sent,t.variableNames=i.variableNames,t.chooseData=i.chooseData,t.chooseData.variableName=t.seriesname,t.chartData=i.chartData,t.myTableDatas=i.myTableDatas,t.refreshArcbar(),t.refreashArea();case 11:case"end":return a.stop()}}),a)})))()},reload:function(e,t){this.loadfive(e),this.seriesname=t,this.chartData.series[0].name=this.seriesname},loadhistory:function(){var e=this;return s(n.default.mark((function t(){var a,r;return n.default.wrap((function(t){while(1)switch(t.prev=t.next){case 0:return a={PageSize:50,IsGas:e.isgas},t.next=3,e.$u.api.hisindex(a);case 3:r=t.sent,e.historyData=r;case 5:case"end":return t.stop()}}),t)})))()},loaderror:function(){var e=this;return s(n.default.mark((function t(){var a,r;return n.default.wrap((function(t){while(1)switch(t.prev=t.next){case 0:return a={PageSize:50,IsGas:e.isgas},t.next=3,e.$u.api.errorindex(a);case 3:r=t.sent,e.errorData=r;case 5:case"end":return t.stop()}}),t)})))()}},mounted:function(){}};t.default=o}).call(this,a("543d")["default"])}},[["8b86","common/runtime","common/vendor"]]]);