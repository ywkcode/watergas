<template>

	<view class="charts-box">
		<qiun-data-charts type="area" :chartData="chartDataInner" background="none" :opts="opts" />
	</view>
</template>

<script>
	export default {
		name: "myarea",
		props: {
			testdata: {
				type: Object
			},
			chartDataInner: {
				categories: [],
				series: {
					name: "TOC",
					data: []
				},
			},
		},
		data() {
			return {
				title: '',
				chartData: {    },

				opts: {}
			}
		},

		created() {
			//调用绘制方法
			console.log("我是area")
			var that = this;
			this.title = that.testdata.title;
			this.getServerData()
		},

		methods: {
			getServerData() {

			 
			 
				//this.chartData  =this.chartDataInner;
				// {
				//                 "categories":
				// 				 [
				//                         "2016",
				//                         "2017",
				//                         "2018",
				//                         "2019",
				//                         "2020",
				//                         "2021"
				//                     ],
				//                     "series": [
		 	//                         {
				//                             "name": "成交量A",
				//                             "data": [
				//                                 35,
				//                                 8,
				//                                 25,
				//                                 37,
				//                                 4,
				//                                 20
				//                             ]
				//                         }
				//                     ]
				//             }

				this.opts = {
					"type": "area",
					"canvasId": "",
					"canvas2d": false,
					"background": "none",
					"animation": true,
					"timing": "easeOut",
					"duration": 1000,
					"color": [
						"#1890FF",
						"#91CB74",
						"#FAC858",
						"#EE6666",
						"#73C0DE",
						"#3CA272",
						"#FC8452",
						"#9A60B4",
						"#ea7ccc"
					],
					"padding": [
						15,
						15,
						0,
						15
					],
					"rotate": false,
					"errorReload": true,
					"fontSize": 13,
					"fontColor": "#666666",
					"enableScroll": false,
					"touchMoveLimit": 60,
					"enableMarkLine": false,
					"dataLabel": true,
					"dataPointShape": true,
					"dataPointShapeType": "solid",
					"tapLegend": true,
					"xAxis": {
						"disabled": false,
						"axisLine": true,
						"axisLineColor": "#CCCCCC",
						"calibration": false,
						"fontColor": "#666666",
						"fontSize": 13,
						"rotateLabel": false,
						"itemCount": 5,
						"boundaryGap": "center",
						"disableGrid": true,
						"gridColor": "#CCCCCC",
		 			"gridType": "solid",
						"dashLength": 4,
						"gridEval": 1,
						"scrollShow": false,
						"scrollAlign": "left",
						"scrollColor": "#A6A6A6",
						"scrollBackgroundColor": "#EFEBEF",
						"format": ""
					},
					"yAxis": {
						"disabled": false,
						"disableGrid": false,
						"splitNumber": 5,
						"gridType": "dash",
						"dashLength": 2,
						"gridColor": "#CCCCCC",
						"padding": 10,
						"showTitle": false,
						"data": []
					},
					"legend": {
						"show": true,
						"position": "bottom",
						"float": "center",
						"padding": 5,
		 			"margin": 5,
						"backgroundColor": "rgba(0,0,0,0)",
						"borderColor": "rgba(0,0,0,0)",
						"borderWidth": 0,
						"fontSize": 13,
						"fontColor": "#666666",
						"lineHeight": 11,
						"hiddenColor": "#CECECE",
						"itemGap": 10
					},
					"extra": {
						"area": {
							"type": "straight",
							"opacity": 0.2,
							"addLine": true,
							"width": 2,
							"gradient": false
						},
						"tooltip": {
							"showBox": true,
							"showArrow": true,
							"showCategory": false,
							"borderWidth": 0,
							"borderRadius": 0,
							"borderColor": "#000000",
							"borderOpacity": 0.7,
							"bgColor": "#000000",
							"bgOpacity": 0.7,
							"gridType": "solid",
							"dashLength": 4,
							"gridColor": "#CCCCCC",
							"fontColor": "#FFFFFF",
							"splitLine": true,
							"horizentalLine": false,
							"xAxisLabel": false,
							"yAxisLabel": false,
							"labelBgColor": "#FFFFFF",
							"labelBgOpacity": 0.7,
							"labelFontColor": "#666666"
						},
						"markLine": {
							"type": "solid",
							"dashLength": 4,
							"data": []
						}
					}
				}

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
