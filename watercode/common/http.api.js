// /common/http.api.js

// 如果没有通过拦截器配置域名的话，可以在这里写上完整的URL(加上域名部分)

let indexUrl = '/api/index';
let hisUrl = '/api/Data/history';
let loginUrl="/api/Data/login";
let loadfiveUrl="/api/Data/five"
// 此处第二个参数vm，就是我们在页面使用的this，你可以通过vm获取vuex等操作，更多内容详见uView对拦截器的介绍部分：
// https://uviewui.com/js/http.html#%E4%BD%95%E8%B0%93%E8%AF%B7%E6%B1%82%E6%8B%A6%E6%88%AA%EF%BC%9F
const install = (Vue, vm) => {
	// 此处没有使用传入的params参数
	let index = (params = {}) => vm.$u.get(indexUrl);
	let hisindex = (params) => vm.$u.get(hisUrl,params);
	let loginindex= (params) => vm.$u.get(loginUrl,params);
	let loadfiveindex= (params) => vm.$u.get(loadfiveUrl,params);
	vm.$u.api = {
		index,
		hisindex,
		loginindex,
		loadfiveindex
	};
	//或者写成
	// vm.$u.api={}
	// vm.$u.api.login = parames => vm.$u.post('/api/auth/login', parames)
	// vm.$u.api.myhistory = (parames) => vm.$u.get('/api/Data/history', parames)
	//vm.$u.api.authIndex=params=>vm.$u.post('/api/auth/login',params)
}

export default {
	install
}
