<template>
    <div class="ListData">
            <div class="row m-4">
                <div class="col-lg-12 col-sm-12 ml-auto mr-auto">
                <div class="card ">
                    <div class="card-header">
                        <div class="row ml-1">
                            <h4 class="card-title DayHeading">Company Details</h4>
                        </div>
                        <div class="row">
                            
                                <div class="col-md-4 m-1 " v-bind:class="{'has-danger' : v$.companySearch.$error}">
                                    
                                    <input class="form-control" v-model="v$.companySearch.$model" type="text" placeholder="Search By Company Name"  />
                                    <span v-if="v$.companySearch.$error" class="error">
                                        <span v-if="!v$.companySearch.required">Name is required</span>
                                        <span v-if="!v$.companySearch.maxLength">Name not more than 20</span>
                                    </span>
                                </div>
                           
                        </div>
                    </div>
                </div>
                    <div class="card">
                        <div class="card-body">
                            <div class="col-lg-12">
                                <div class="row">
                                    <a href="javascript:void(0)" class="btn btn-outline-primary " v-on:click="AddCompany"><i class="fa fa-plus"></i> Add </a>
                                    <!-- <div v-if="companyList.length == 0">
                                        <a href="javascript:void(0)" class="btn btn-outline-primary " v-on:click="AddCompany"><i class="fa fa-plus"></i> Add </a>
                                    </div>
                                    <div v-else>
                                        <a href="javascript:void(0)" class="btn btn-outline-primary  disabled" v-on:click="AddCompany"><i class="fa fa-plus"></i> Add </a>
                                    </div> -->
                                </div>
                                <div class="mt-2">
                                    <div class=" table-responsive">
                                        <table class="table ">
                                            <thead class="m-0">
                                                <tr>
                                                    <th>
                                                        Name
                                                    </th>
                                                    <th>
                                                        Reg. No.
                                                    </th>
                                                    <th>
                                                        Vat No.
                                                    </th>
                                                    <th>
                                                        Mobile
                                                    </th>
                                                    <th>
                                                        Email
                                                    </th>
                                                    <th>
                                                        Website
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="details in companyList" v-bind:key="details.id">
                                                    <td>
                                                         <strong>
                                                            <a href="javascript:void(0)" v-on:click="EditCompanyInfo(details.id)">{{details.nameArabic}}</a>
                                                        </strong>
                                                    </td>
                                                    <td>{{details.commercialRegNo}}</td>
                                                    <td>{{details.vatRegistrationNo}}</td>
                                                    <td>{{details.mobile}}</td>
                                                    <td>{{details.email}}</td>
                                                    <td>{{details.website}}</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
    </div>
</template>
<script>
    // import { required, maxLength } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import { required, maxLength } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    export default {
        name: 'company',
          setup() {
            return { v$: useVuelidate() }
        },

        data: function () {
            return {
                show: false,
                type: '',
                companySearch: '',
                companyList:[

                ]
            }
        },
        validations() {
        return{
                companySearch: {
                    required,
                    maxLength: maxLength(20)
                }
        }
        },
        methods: {
            GetCompanyData: function () {
                var root = this;
                var url = '/Company/GetCompanyInformation';
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get(url, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.companyList = response.data.companyInformation;
                    }
                });
            },
            AddCompany: function () {
                // this.$router.push('/CompanyAdditionalInfo')

                 this.$router.push({
                path: '/CompanyAdditionalInfo',
                query: {
                    Add: true,
                    isDeliveryChallan: true,

                }
            })
            },
            EditCompanyInfo: function () {
                var root = this;
                root.$router.push({
                    path: '/CompanyAdditionalInfo',
                    query: { 
                        data: '',
                        Add: false,
                        }
                })
            }
        },
        mounted: function () {
            this.GetCompanyData();
        }
    }
</script>