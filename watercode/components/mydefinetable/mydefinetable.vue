<template>
	<view>
		<uni-table stripe emptyText="暂无更多数据">
			<!-- 表头行 -->
			<uni-tr>
				<uni-th v-for="(item,index) in historyData.namelists" align="center"  width="80rpx" >
					{{item}}
				</uni-th>
			</uni-tr>
			<!-- 表格数据行 -->
			<uni-tr v-for="(item,index) in historyData.historyTableResponses">
				<uni-td align="center" >{{item.data1}}</uni-td>
				<uni-td align="center"   >{{item.data2}}</uni-td>
				<uni-td align="center"   >{{item.data3}}</uni-td>
				<uni-td align="center"   >{{item.data4}}</uni-td>
				<uni-td align="center"  v-if="item.data5">{{item.data5}}</uni-td>
				<uni-td align="center"  >{{item.createTime}}</uni-td>
			    <uni-td align="center" >{{item.isCorrect==false?"正常":"异常"}}</uni-td>
			</uni-tr>

		</uni-table>
	</view>
</template>

<script>
	export default {
		name: "mydefinetable",
		props: {
			historyData:{
				historyTableResponses:{},
				namelists: []
			}
		 
		},
		data() {
			return {
				//mytableData:[]
			};
		},
		created() {
           
		}
	}
</script>

<style>

</style>
