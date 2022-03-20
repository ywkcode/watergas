<template>
	
	<view class="charts-box">  
	    <qiun-data-charts type="gauge" :chartData="chartData" background="none" :opts="opts" />
	</view> 
</template>

<script>
	 
	export default {
	    name:"gauge",
		props:{
			testdata:{
				type:Object
			}
		} ,
		data() 
			{
			   return {
				   title: '',
				   chartData: {
					   categories: [],
					   series: [],
				   },
								 
				   opts: {}
			   }
			},
		  
		    created() {
		        //调用绘制方法
					console.log("我是gauge")
			    var that=this;
				this.title=that.testdata.title;
		        this.getServerData()
		    },
			  
		    methods: {
		        getServerData() {
		          
					 
		                this.chartData = 
						{
		                    categories: [
										  {
										            "value": 0.2,
										            "color": "#1890ff"
										        },
										        {
										            "value": 0.8,
										            "color": "#2fc25b"
										        },
										        {
										            "value": 1,
										            "color": "#f04864"
										        }
		                    ],
		                    series: [{
		                        "name": "TOC",
		                        "data": 0.5
		                    }]
		                }
			  
		                this.opts = 
						{
		                    "type": "gauge",
		                    "canvasId": "111222",
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
		                    "rotate": false,
		                    "errorReload": true,
		                    "fontSize": 13,//刻度字体大小
		                    "fontColor": "#666666",
		                    "enableScroll": false,
		                    "touchMoveLimit": 60,
		                    "enableMarkLine": false,
		                    "dataLabel": true,
		                    "dataPointShape": true,
		                    "dataPointShapeType": "solid",
		                    "tapLegend": true,
		                    "title": {
		                        "name": this.title,
		                        "fontSize": 13,
		                        "color": "#2fc25b",
		                        "offsetX": 0,
		                        "offsetY": 30
		                    },
		                    "subtitle": {
		                        "name": "TOC",
		                        "fontSize": 13,
		                        "color": "#1890ff",
		                        "offsetX": 0,
		                        "offsetY": 35,//调整纵向距离
		                    },
		                    "extra": {
		                        "gauge": {
		                            "type": "default",
		                            "width": 30,//环形宽度
		                            "labelColor": "#91B1EC",
		                            "labelOffset": 10,//label标注与环形的距离
		                            "startAngle": 0.75,
		                            "endAngle": 0.25,
		                            "startNumber": 0,
		                            "endNumber": 10,
		                            "format": "",
		                            "splitLine": {
		                                "fixRadius": 0,
		                                "splitNumber": 4,//分割段数
		                                "width": 30,
		                                "color": "#FFFFFF",//去掉分割线
		                                "childNumber": 5,
		                                "childWidth": 12
		                            },
		                            "pointer": {
		                                "width": 24,
		                                "color": "#1890ff"
		                            },
		                            "labelFormat": ""
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
