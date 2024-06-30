<template>
    <modal :modalLarge="true" :show="show" >
        <div class="modal-content">
            <div class="modal-header" >
             
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" >
                    Branches
                </h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
           
            <div class="modal-body" >

                    <div class="table-responsive">
                            <table class="table mb-0">
                                <thead class="thead-light table-hover">
                                    <tr>
                                        <th>#</th>
                                        <th>
                                            {{ $t('Branches.Code') }}
                                        </th>
                                        <th>
                                            {{ $t('Branches.BranchName') }}
                                        </th>
                                        <th>
                                            Location Name
                                        </th>
                                        <th>
                                        Branch Type
                                    </th>
                                        <th>
                                            {{ $t('Branches.ContactNo') }}
                                        </th>
                                        <th>
                                            {{ $t('Branches.Address') }}
                                        </th>
                                        <th>
                                            {{ $t('Branches.City') }}
                                        </th>
                                        <th>
                                            Main Branch
                                        </th>
                                        <th>
                                            Centerlized
                                        </th>
                                        <th>
                                            Online
                                        </th>
                                        <th>
                                            Approval
                                        </th>
                                        <th>
                                           Status
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="(terminal,index) in resultQuery" v-bind:key="index">
                                        <td>
                                            {{index+1}}
                                        </td>

                                        <td >
                                            <strong>
                                                <a href="javascript:void(0)" v-on:click="EditTerminal(terminal.id)">{{terminal.code}}</a>
                                            </strong>
                                        </td>
                                       
                                        <td>
                                            {{terminal.branchName}}
                                        </td>
                                        <td>
                                            {{locationName}}
                                        </td>
                                        <td>
                                        {{terminal.branchType}}
                                        </td>
                                        <td>
                                            {{terminal.contactNo}}
                                        </td>
                                        <td>
                                            {{terminal.address}}
                                        </td>
                                        <td>
                                            {{terminal.city}}
                                        </td>
                                        <td>
                                            <span v-if="terminal.mainBranch" class="badge badge-boxed  badge-outline-success">Main Branch</span>
                                            <span v-else class="badge badge-boxed  badge-outline-danger"></span>
                                        </td>
                                        <td>
                                            <span v-if="terminal.isCentralized" class="badge badge-boxed  badge-outline-success">Centerlized</span>
                                            <span v-else class="badge badge-boxed  badge-outline-danger">De-Centerlized</span>
                                        </td>
                                        <td>
                                            <span v-if="terminal.isOnline" class="badge badge-boxed  badge-outline-success">Online</span>
                                            <span v-else class="badge badge-boxed  badge-outline-danger">Offline</span>
                                        </td>
                                        <td>
                                            <span v-if="terminal.isApproval" class="badge badge-boxed  badge-outline-success">Approval</span>
                                            <span v-else class="badge badge-boxed  badge-outline-danger">Without Approval</span>
                                        </td>
                                        <td>
                                            <span v-if="terminal.isActive" class="badge badge-boxed  badge-outline-success">{{$t('color.Active')}}</span>
                                            <span v-else class="badge badge-boxed  badge-outline-danger">{{$t('color.De-Active')}}</span>
                                        </td>

                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
            
            
                    <addbranches :newbranch="newBranch"
                         :show="showBranch"
                         v-if="showBranch"
                         @close="GetBranchList()"
                         :type="type" />

           
            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
        </div>
    </modal>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'

    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';

export default {
    mixins: [clickMixin],
    props: ['show','locationId','locationName'],
    components: {
        Loading
    },
    data: function () {
        return {
            resultQuery:[],
            newBranch: {
                    id: '',
                    code: '',
                    branchName: '',
                    contactNo: '',
                    address: '',
                    city: '',
                    state: '',
                    postalCode: '',
                    country: '',
                    isActive: true,
                    isOnline: false,
                    isApproval: false,
                    isCentralized: false,
                    mainBranch: true,
                },
           
            render: 0,
            arabic: '',
            english: '',
            loading: false,
            showBranch: false,
        }
    },
   
        methods: {
            GetBranchList: function () {
                this.showBranch = !this.showBranch;
                this.GetBranch();

            },
            EditTerminal: function (id) {

            var root = this;

            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Branches/GetBranchDetail?id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data) {

                        root.newBranch.id = response.data.id;
                        root.newBranch.code = response.data.code;
                        root.newBranch.branchName = response.data.branchName;
                        root.newBranch.contactNo = response.data.contactNo;
                        root.newBranch.address = response.data.address;
                        root.newBranch.city = response.data.city;
                        root.newBranch.state = response.data.state;
                        root.newBranch.postalCode = response.data.postalCode;
                        root.newBranch.country = response.data.country;
                        root.newBranch.isActive = response.data.isActive;
                        root.newBranch.mainBranch = response.data.mainBranch;
                        root.newBranch.isOnline = response.data.isOnline;
                        root.newBranch.isApproval = response.data.isApproval;
                        root.newBranch.isCentralized = response.data.isCentralized;


                    

                        root.showBranch = !root.showBranch;
                        root.type = "Edit"
                     } else {
                     console.log("error: something wrong from db.");
                    }
                },
                    function (error) {
                        root.loading = false;
                        console.log(error);
                    });

        },
            close: function () {
                
            this.$emit('closeprefix');
        },
            GetBranch: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('Branches/BranchList?isDropdown=true&locationId=' + this.locationId, { 
    headers: { 
        "Authorization": `Bearer ${token}` 
    } 
})                .then(function (response) {
                    if (response.data != null) {
                        
                        root.resultQuery = response.data.results;
                    }
                });
            },

            
        },
        created: function () {
            
           
                this.GetBranch();

            
          

        },
        mounted: function () {
            

        }
   
    
}
</script>
