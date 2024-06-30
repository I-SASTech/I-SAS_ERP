<template>
    <modal :show="show" :modalLarge="true">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" >Transaction Log</h6>
                <button type="button" class="btn-close" v-on:click="onCancel()"></button>
            </div>
            <div class="modal-body">
                                

                                <div class="table-responsive">
                        <table class="table mb-0">
                            <thead class="thead-light table-hover">
                                <tr>
                                    <th>#</th>
                                    <th class="text-center">
                                        Table Name
                                    </th>

                                    <th class="text-center">
                                        Date
                                    </th>
                                    <th class="text-center">
                                        Action
                                    </th>
                                    <th class="text-center">
                                        Push Date
                                    </th>
                                    <th class="text-center">
                                        Pull Date
                                    </th>
                                
                                    <th class="text-center">
                                    Document Number
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(color,index) in list" v-bind:key="color.changeDate">
                                    <td >
                                        {{index+1}}
                                    </td>
                                    
                                
                                    <td class="text-center" >
                                        <strong >
                                            <a href="javascript:void(0)" v-on:click="openmodel(color.code)">  {{formatTableName(color.table)}}</a>

                                        
                                        </strong>
                                    </td>
                                    <td class="text-center">{{GetDate(color.changeDate)}}</td>
                                    <td class="text-center">{{color.action}}</td>
                                    <td class="text-center">{{GetDate(color.pushDate)}}</td>
                                    <td class="text-center">{{GetDate(color.pullDate)}}</td>
                                    <td class="text-center">{{color.documentNo}}</td>

                                
                                </tr>
                            </tbody>
                        </table>
                    </div>
                            

                            </div>
           
                           
    </div>
    </modal>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import moment from 'moment';
    export default {
        name: "PushPullModel",
        mixins: [clickMixin],
       
        props: [ 'show','documentNo','logType' ],
        data: function () {
            return {
                list: [],

               
               
            }
        },
        created() {
            this.GetAutoCodeGenerator()

        },

        methods: {
            formatTableName(tableName) {
                        // Split the tableName by capital letters
                        const words = tableName.split(/(?=[A-Z])/);
                        
                        // Capitalize the first letter of each word and join with a space
                        return words.map(word => word.charAt(0).toUpperCase() + word.slice(1)).join(' ');
                    },


            GetDate: function (date) {
                if(date==null || date=='' )
                {
                    return '';
                }
                return   moment(date).format('lll');

        },
         
            GetAutoCodeGenerator: function () {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Branches/PushPullDetailQuery?documentNumber=' + this.documentNo+ '&logType=' + this.logType, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.list= response.data;
                    }
                });
            },

          
            onCancel: function () {
                this.$emit('close');
            },
        },
        mounted: function () {

           
        }
    }
</script>