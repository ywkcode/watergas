(global["webpackJsonp"]=global["webpackJsonp"]||[]).push([["pages/index/index"],{"3ef4":function(n,e,t){},"4e33":function(n,e,t){"use strict";(function(n){Object.defineProperty(e,"__esModule",{value:!0}),e.default=void 0;var u=i(t("a34a"));function i(n){return n&&n.__esModule?n:{default:n}}function r(n,e,t,u,i,r,a){try{var c=n[r](a),o=c.value}catch(f){return void t(f)}c.done?e(o):Promise.resolve(o).then(u,i)}function a(n){return function(){var e=this,t=arguments;return new Promise((function(u,i){var a=n.apply(e,t);function c(n){r(a,u,i,c,o,"next",n)}function o(n){r(a,u,i,c,o,"throw",n)}c(void 0)}))}}var c={data:function(){return{list:[{image:"../../../static/tab1.png",title:"昨夜星辰昨夜风，画楼西畔桂堂东"},{image:"../../../static/tab2.png",title:"身无彩凤双飞翼，心有灵犀一点通"}],title:"行业咨询"}},onLoad:function(){return a(u.default.mark((function n(){return u.default.wrap((function(n){while(1)switch(n.prev=n.next){case 0:case"end":return n.stop()}}),n)})))()},onBackPress:function(e){return n.navigateTo({url:"/pages/login/index"}),!0},onNavigationBarButtonTap:function(e){n.reLaunch({url:"/pages/login/index"})},methods:{click:function(n){this.$refs.uToast.success("点击了第".concat(n,"个"))},goNext:function(e){1==e?n.reLaunch({url:"/pages/center/index?isgas=1"}):n.reLaunch({url:"/pages/center/index?isgas=0"})}}};e.default=c}).call(this,t("543d")["default"])},"5ede":function(n,e,t){"use strict";(function(n){t("8c7d");u(t("66fd"));var e=u(t("aa96"));function u(n){return n&&n.__esModule?n:{default:n}}wx.__webpack_require_UNI_MP_PLUGIN__=t,n(e.default)}).call(this,t("543d")["createPage"])},7492:function(n,e,t){"use strict";var u=t("3ef4"),i=t.n(u);i.a},aa96:function(n,e,t){"use strict";t.r(e);var u=t("bea9"),i=t("e8f9");for(var r in i)"default"!==r&&function(n){t.d(e,n,(function(){return i[n]}))}(r);t("7492");var a,c=t("f0c5"),o=Object(c["a"])(i["default"],u["b"],u["c"],!1,null,"3c6bd4f5",null,!1,u["a"],a);e["default"]=o.exports},bea9:function(n,e,t){"use strict";t.d(e,"b",(function(){return i})),t.d(e,"c",(function(){return r})),t.d(e,"a",(function(){return u}));var u={uSwiper:function(){return t.e("uview-ui/components/u-swiper/u-swiper").then(t.bind(null,"db6c"))},uLine:function(){return t.e("uview-ui/components/u-line/u-line").then(t.bind(null,"5675"))},uIcon:function(){return t.e("uview-ui/components/u-icon/u-icon").then(t.bind(null,"94a2"))}},i=function(){var n=this,e=n.$createElement;n._self._c},r=[]},e8f9:function(n,e,t){"use strict";t.r(e);var u=t("4e33"),i=t.n(u);for(var r in u)"default"!==r&&function(n){t.d(e,n,(function(){return u[n]}))}(r);e["default"]=i.a}},[["5ede","common/runtime","common/vendor"]]]);