<template>
	<view>

		<u-subsection :list="list" :current="0" @change="sectionChange"></u-subsection>
		<view v-if="firstTab">
			<myarcbar :testdata="mytestdata"  ref="myarcbar"
			:seriesname="chooseData.variableName" 
			:seriesdata="chooseData.chosenPercent" 
			:titlename="chooseData.chosenData">
			</myarcbar>
			
			<view class="myouttext" v-if="isgas">
				<view class="mytext"  @click="reload('H2S','硫化氢')">
					<view class="mytext_inner" >硫化氢</view>
					<u-tag :text="variableNames.h2S" plainFill plain type="success"></u-tag>
				</view>
				<view class="mytext" @click="reload('HCL','氯化氢')">
					<view class="mytext_inner"  >氯化氢</view>
					<u-tag :text="variableNames.hcl" plainFill type="success"></u-tag>
				</view>
				<view class="mytext"  @click="reload('CL2','氯气')">
					<view class="mytext_inner" >氯气</view>
					<u-tag :text="variableNames.cL2" plainFill type="success"></u-tag>
				</view>
				<view class="mytext"  @click="reload('NH3','氨气')">
					<view class="mytext_inner" >氨气</view>
					<u-tag :text="variableNames.nH3" plainFill type="success"></u-tag>
				</view> 
			</view>
			
			<view class="myouttext" v-if="!isgas"> 
				<view class="mytext" @click="reload('TOC','Toc')">
					<view class="mytext_inner">TOC</view>
					<u-tag :text="variableNames.toc" plainFill plain type="success"></u-tag>
				</view>
				<view class="mytext"  @click="reload('AD','氨氮')">
					<view class="mytext_inner">氨氮</view>
					<u-tag :text="variableNames.ad" plainFill type="success"></u-tag>
				</view>
				<view class="mytext" @click="reload('ZL','总磷')">
					<view class="mytext_inner">总磷</view>
					<u-tag :text="variableNames.zl" plainFill type="success"></u-tag>
				</view>
				<view class="mytext" @click="reload('PH','PH')">
					<view class="mytext_inner">PH</view>
					<u-tag :text="variableNames.ph" plainFill type="success"></u-tag>
				</view>
				<view class="mytext" @click="reload('LL','流量')">
					<view class="mytext_inner">流量</view>
					<u-tag :text="variableNames.ll" plainFill type="success"></u-tag>
				</view>
			</view>

			<u-gap height="10" bgColor="#bbb"></u-gap>
			
			<view class="u-padding-20 mytitle">实时折线图</view>
			<u-gap height="3" bgColor="#bbb"></u-gap>
			<myarea 
			:testdata="mytestdata"
			:chartDataInner="chartData" ref="myarea"></myarea>

			<u-gap height="10" bgColor="#bbb"></u-gap>
			<view class="u-padding-20 mytitle">实时监测数据</view>
			<u-gap height="3" bgColor="#bbb"></u-gap>
			<mytable :mytableData="myTableDatas"></mytable>
		</view>
		<view v-if="secondTab">
			<mydefinetable :historyData="historyData"></mydefinetable>
		</view>
		<view v-if="thirdTab">
			<mydefinetable :historyData="errorData"></mydefinetable>
		</view>
	</view>
</template>
import  myarcbar from "@/components/myarcbar/myarcbar"
<script>
	export default {
		data() {
			return {
				title: 'Hello',
				chartData: {
					categories: [],
					series: {
						name:"TOC",
						data:[]
					},
				},
				mytestdata: {
					title: '测试1'
				},
				opts: {},
				list: ['实时数据', '历史数据', '异常数据'],
				// 或者如下，也可以配置keyName参数修改对象键名
				// list: [{name: '未付款'}, {name: '待评价'}, {name: '已付款'}],
				current: 0,
				firstTab: true,
				secondTab: false,
				thirdTab: false,
				isgas: false, 
				seriesname: 'TOC',
				seriesdata: 0.7,
				titlename: '03mh',
				mytablist: [],
				variableNames: {
					ad: "0.000",
					cL2: "0.000",
					h2S: "0.000",
					hcl: "0.000",
					ll: "0.000",
					nH3: "0.000",
					ph: "0.000",
					toc: "0.000",
					zl: "0.000"
				},
				chooseData:{
					variableName:"TOC",
					chosenData:"10",
					chosenPercent:0.5
				},
				myTableDatas:[],
				historyData:{
					namelists:{},
					historyTableResponses:[]
				},
				errorData:{
					namelists:{},
					historyTableResponses:[]
				}
			}
		},
		//加载页
		onLoad: function(option) {
			 
			 this.isgas=(option.isgas=="1"?true:false) 
			 if(this.isgas==true) this.seriesname="H2S";
			  if(this.isgas==false) this.seriesname="TOC";
			 this.loadfive(this.seriesname);
		},
		onBackPress(e) {
			uni.navigateTo({
				url: '/pages/index/index'
			})
			return true;
		},
		onReady() {
			//调用绘制方法
			this.mytestdata = {
				title: 'ceshi'
			}

		},

		methods: {
			click(item) {
				console.log('item', item);
			},
			sectionChange(index) {
				this.firstTab = false;
				this.secondTab = false;
				this.thirdTab = false;
				if (index == 0) 
				{
					this.firstTab = true;
					loadfive(this.seriesname)
				}
				if (index == 1) 
				{
					this.secondTab = true;
					this.loadhistory();
				}
				if (index == 2) 
				{
					 
					this.thirdTab = true;
					this.loaderror();
				}
				 
			},
			refreshArcbar()
			{
				 
				setTimeout(()=>{
					this.$refs.myarcbar.getServerData(); 
				},100)
				
			},
			refreashArea()
			{
				setTimeout(()=>{ 
					this.$refs.myarea.getServerData();
				},100)
			},
			async  loadfive(value) {
			 
				var paramedata = {
					VariableName: value,
					IsGas: this.isgas
				};
				var fivadata =  await this.$u.api.loadfiveindex(paramedata);
				this.variableNames = fivadata.variableNames; 
				this.chooseData=fivadata.chooseData; 
				this.chooseData.variableName=this.seriesname;
				//折线图
				this.chartData=fivadata.chartData;
			
				//数据列表
				this.myTableDatas=fivadata.myTableDatas; 
				this.refreshArcbar();
			    this.refreashArea();
			},
			 
			reload(value,variablename)
			{
				this.loadfive(value); 
				this.seriesname=variablename; 
				this.chartData.series[0].name=this.seriesname;
			},
			async  loadhistory() {
			 
			    var paramedata = {
			    	PageSize: 50,
			    	IsGas: this.isgas
			    }; 
				var hisdata =  await this.$u.api.hisindex(paramedata);
			 
			    this.historyData=hisdata;
			},
			async  loaderror() {
			 
			    var paramedata = {
			    	PageSize: 50,
			    	IsGas: this.isgas
			    }; 
				var hisdata =  await this.$u.api.errorindex(paramedata);
			 
			    this.errorData=hisdata;
			}
 
		},

		mounted() {

		}
	}
</script>

<style lang="scss" scoped>
	@mixin flexCenter {
		display: flex;
		justify-content: center;
		align-items: center;
	}

	.myouttext {
		padding: 10rpx;
		text-align: center;
	}

	.mytext {
		display: inline-block;
		padding: 10rpx;
	}

	.mytext_inner {
		padding-bottom: 5rpx;
	}

	.mytag {
		padding: 0 60rpx 0 60rpx;
	}

	.mytitle {
		text-align: center;
	}

	.mytab_inner {
		margin: 0 10rpx 10rpx 0;
	}

	.content {
		width: 100%;
		height: 100%;
		background: #fff;

		.warnInfo {
			width: 100%;
			height: 300upx;
			background: #132040;

			font-size: 30upx;
			color: #fff;

			.warn_title {
				width: 100%;
				height: 60upx;
				line-height: 60upx;
				text-align: left;
				background: #182951;
				padding: 0 40upx;
				box-sizing: border-box;
			}

			.warn_echart {
				width: 100%;
				padding: 0 40upx;
				box-sizing: border-box;
				height: calc(100% - 60upx) !important;
				color: #fff;

				.charts_box {
					width: 100%;
					height: 100%;
				}
			}
		}
	}
</style>
