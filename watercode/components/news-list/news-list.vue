<template>
	<!-- url="/pages/news/newsdetails/newsdetails" -->  
	<navigator 
	hover-class="navigator-hover">
	  <u-row gutter="16">
		<u-col span="3">
			<view>
				<u-image width="150rpx" height="150rpx"  :src="item.newsImgUrl" class="u-flex-nowrap" @tap="MyClick(item.title)">
				</u-image>
			</view>
		</u-col>
		<u-col span="9">
			<view class="info_title u-line-1 u-text-left u-col-top">{{item.title}}</view>
			<view class="info_content u-line-2 u-text-left u-col-top">{{item.description}}</view>
			<view class="info_date u-text-right u-col-top">{{item.newsDate}}</view>
		</u-col>
	</u-row>  
	</navigator>
</template>

<script>
	export default {
		name:"news-list",
		props:
		{  
			item:{
				type:[Object,Number] 
			}
		},
		data() {
			return {
				
			};
		},
		methods:{
			MyClick(e){ 
				this.$emit("changes",e)
			}
		}
	}
</script>

<style lang="scss" scoped> 
	
   .info-img {
		width: 50rpx;
		height: 50rpx;
	}

	.info_title {
		font-weight: 700;
		font-size: 20rpx;
	}

	.info_content {
		font-size: 20rpx;
		text-indent: 2em;
	}

	.info_date {
		font-size: 20rpx;
		color: $u-main-color;
	}

</style>
