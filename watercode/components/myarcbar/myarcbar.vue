<template>
	
	<view class="charts-box">  
	    <qiun-data-charts type="arcbar" 
		  :chartData="chartData" 
		  background="none" 
          :opts="opts" />
	</view> 
</template>

<script>
	 
	export default {
	    name:"myarcbar",
		props:{
			testdata:{
				type:Object
			},
			seriesname:{type:String},
			seriesdata:{type:Number},
			titlename:{type:String}
		} ,
		data() 
			{
			   return {
				   title: '',
				   chartData: { 
					   series: [],
				   }, 
				   opts: {}
			   }
			},
		  
		    created() {
		        
			    var that=this;
				this.title=that.testdata.title;
			 
		        this.getServerData()
		    },
			  
		    methods: {
		        getServerData() {  
					 
		                this.chartData = 
						{
		                       "series": [
		                             {
		                                 "name": this.seriesname,
		                                 "data": this.seriesdata,
		                                 "color": "#2fc25b"
		                             }
		                         ]
		                }
			  
		                this.opts = 
						{
		                         "type": "arcbar",
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
		                         "title": {
		                             "name": this.titlename,
		                             "fontSize": 15,
		                             "color": "#3CA272",
		                             "offsetX": 0,
		                             "offsetY": 0
		                         },
		                         "subtitle": {
		                             "name": this.seriesname,
		                             "fontSize": 18,
		                             "color": "#666666",
		                             "offsetX": 0,
		                             "offsetY": 0
		                         },
		                         "extra": {
		                             "arcbar": {
		                                 "type": "circle",
		                                 "width": 6,
										 "radius": 75,
		                                 "backgroundColor": "#E9E9E9",
		                                 "startAngle": 0.75,
		                                 "endAngle": 0.25,
		                                 "gap": 2,
		                                 "centerX": 0,
		                                 "centerY": 0,
		                                 "linearType": "none"
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
