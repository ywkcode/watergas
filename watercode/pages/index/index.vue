<template>
	<view>
		<!-- <u-navbar  
		 title="修水赣北钨业有限公司-总排口监测点"
		 :autoBack="true" >
		</u-navbar> -->
		<view class="wrap">
			<u-swiper :list="list" height="320"></u-swiper>
		</view>
	     <view class="u-padding-20 mytitle">数据指标</view>
	      <u-line color="#bbb"></u-line>
		  <!-- <view>
		 <u-icon name="photo" color="#2979ff" size="128"
		  label="水质" labelPos="bottom" space="3px" labelColor="gray"></u-icon>
		  <p style="display:inline-block;">1111</p>
		 </view> -->
		 
		 				 <view style="margin: 20px;height: 60px;width:100%;position: relative;" @click.native="goNext(!true)">
							 <image src="../../static/water.png" style="width:60px;height: 60px;display: inline-block;"></image>
		 				 			  <!-- <view style="display: inline-block; width:60px;height: 60px;background-image: url(../../static/water.png); background-size: cover;"></view> -->
		 				 			  <view style="display: inline-block;width:80%;height: 60px;float:right;text-overflow: clip;">
		 				 				  <p style="font-weight: bolder">水质监测</p>
		 				 				  <p style="text-overflow: clip;">TOC,AD,ZL,LL</p>
		 				 				  <span style="position: absolute;right: 24px;bottom: 20px;"><u-icon name="arrow-right" color="#2979ff" size="28"></u-icon></span>
		 				 			  </view>
		 				 </view>
		 				 
		
		  
		      <view style="margin: 20px;height: 60px;width:100%;position: relative;"  @click.native="goNext(true)">
			   <image src="../../static/gas.png" style="width:60px;height: 60px;display: inline-block;"></image>
		 			  <!-- <view style="display: inline-block; width:60px;height: 60px;background-image: url(../../static/gas.png); background-size: cover;"></view> -->
		 			  <view style="display: inline-block;width:80%;height: 60px;float:right;text-overflow: clip;">
		 				  <p style="font-weight: bolder">气体监测</p>
		 				  <p style="text-overflow: clip;">HCL,氨气,CO2,HCL</p>
						    <span style="position: absolute;right: 24px;bottom: 20px;"><u-icon name="arrow-right" color="#f56c6c" size="28"></u-icon></span>
		 			  </view>
					  
		      </view> 
	</view>
	

</template>

<script>
	export default {
		data() {
			return {
					 
				list: [{
						image: '../../static/tab1.png',
						title: '昨夜星辰昨夜风，画楼西畔桂堂东'
					},
					{
						image: '../../static/tab2.png',
						title: '身无彩凤双飞翼，心有灵犀一点通'
					} 
					 
				],
				  
				 title:"行业咨询"
			}
		},
		async onLoad() { 
			  
		},
		onBackPress(e) {
		    uni.navigateTo({
		        url:'/pages/login/index'
		    })
		    return true;
		},
		onNavigationBarButtonTap(e){
			uni.reLaunch({
				url: '/pages/login/index'
			});
		},
		methods: {
            click(name) {
                           this.$refs.uToast.success(`点击了第${name}个`)
                       } ,
			  goNext (value) { 
				  
				         if(value==true) 
						 {
							  uni.reLaunch({
							  	url: '/pages/center/index?isgas=1'
							  });
						 }
						 else
						 {
							uni.reLaunch({
								url: '/pages/center/index?isgas=0'
							});  
						 } 
					   }
		}
	}
</script>

<style lang="scss" scoped>
	.grid-text {
	        font-size: 14px;
	        color: #909399;
	        padding: 10rpx 0 20rpx 0rpx;
	        /* #ifndef APP-PLUS */
	        box-sizing: border-box;
	        /* #endif */
	    }
	.content {
		display: flex;
		flex-direction: column;
		align-items: center;
		justify-content: center;
		padding: 40rpx;
	}

	.logo {
		height: 200rpx;
		width: 200rpx;
		margin-top: 100rpx;
		margin-left: auto;
		margin-right: auto;
		margin-bottom: 50rpx;
	}

	.text-area {
		display: flex;
		justify-content: center;
	}

	.title {
		font-size: 28rpx;
		color: $u-content-color;
	}

	.button-demo {
		margin-top: 80rpx;
	}

	.link-demo {
		margin-top: 80rpx;
	}
	.mytitle{
		text-align: left;
		font-size:18px;
	    margin: 10px 5px;
	}
	.myul{
		list-style-type: none;
	}
	.mystylediv
	{
		width:60px;
		height: 60px;
		
	}
	.mystylediv img{
		width:100% !important;
		height: 100% !important;
		background-size: 0px;
	}
	 
</style>
