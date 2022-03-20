<template>
	<view>
		<uni-table   stripe emptyText="暂无更多数据" >
				  		     <!-- 表头行 -->
				  		     <uni-tr>
				  		         <uni-th align="center">监测日期</uni-th>
				  		         <uni-th align="center">监测数据</uni-th>
				  		         <uni-th align="center">状态描述</uni-th>
				  		     </uni-tr>
				  		     <!-- 表格数据行 -->
				  		     <uni-tr  v-for="(item,index) in mytableData">
				  		         <uni-td align="center">{{item.createTime}}</uni-td>
				  		         <uni-td align="center">{{item.currentData}}</uni-td>
				  		         <uni-td align="center">{{item.isCorrect==true?"正常":"异常"}}</uni-td>
				  		     </uni-tr>
				  		  <!-- <uni-tr>
				  		       <uni-td align="center">2022-03-02 17:02</uni-td>
				  		       <uni-td align="center">13</uni-td>
				  		       <uni-td align="center">正常</uni-td>
				  		   </uni-tr>
				  		<uni-tr>
				  		    <uni-td align="center">2022-03-02 17:02</uni-td>
				  		    <uni-td align="center">13</uni-td>
				  		    <uni-td align="center">正常</uni-td>
				  		</uni-tr>
				  		<uni-tr>
				  		    <uni-td align="center">2022-03-02 17:02</uni-td>
				  		    <uni-td align="center">13</uni-td>
				  		    <uni-td align="center">正常</uni-td>
				  		</uni-tr> -->
				  	  </uni-table>
	</view>
</template>

<script>
	export default {
		name:"mytable",
		props:{
			mytableData:[]
		},
		data() {
			return {
				//mytableData:[]
			};
		},
		created()   {
		 
		
		}
	}
</script>

<style>

</style>
