<template>
    <div class="row" v-if="isValid('CanViewInquirySetup')">

        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('InquirySetupList.InquirySetupList') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('InquiryList.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('InquirySetupList.InquirySetupList') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-if="isValid('CanAddInquirySetup')" v-on:click="openmodel()" href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('color.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);" class="btn btn-sm btn-outline-danger">
                                    {{ $t('color.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card">
                <div class="card-header">
                    <div class="row">
                        <div class="col-lg-8" style="padding-top:20px">
                    <div class="input-group">
                        <button class="btn btn-secondary" type="button" id="button-addon1"><i class="fas fa-search"></i></button>
                        <input v-model="search" type="text" class="form-control" :placeholder="$t('InquirySetupList.SearchbySetup')" aria-label="Example text with button addon" aria-describedby="button-addon1">
                    </div>
                </div>
                <div class=" col-lg-4 mt-1" v-if="!advanceFilters">

<button v-on:click="search22(true)" type="button" class="btn btn-outline-primary mt-3">
    {{ $t('Sale.ApplyFilter') }}
</button>
<button v-on:click="clearData(false)" type="button"
class="btn btn-outline-primary mx-2 mt-3">
{{ $t('Sale.ClearFilter') }}
</button>

</div>
            </div>
                </div>
                <div class="card-body">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 ">
                        <div class="row" style="padding: 12px 7px; background-color: #E1ECFF; color: #060606; margin: unset;">
                            <div class="col-12" style="display: flex; ">
                                <div style="width:4%; font-weight:bolder;text-align:center;color:black !important; "></div>
                                <div style="width:4%; font-weight:bolder;text-align:center;color:black !important; "><span>#</span></div>
                                <div style="width:15%;text-align:center;font-weight:bold;color:black !important;">{{ $t('InquirySetupList.Code') }}</div>
                                <div style="width:20%;text-align:center;font-weight:bold;color:black !important;">{{ $t('InquirySetupList.Name') }}</div>
                                <div style="width:20%;text-align:center;font-weight:bold;color:black !important;">{{ $t('InquirySetupList.Label') }}</div>
                                <div style="width:26%;text-align:center;font-weight:bold;color:black !important;">{{ $t('InquirySetupList.Description') }}</div>
                                <div style="width:10%;text-align:center;font-weight:bold;color:black !important;">{{ $t('InquirySetupList.Status') }}</div>
                            </div>
                        </div>

                        <draggable class="dragArea list-group w-full" v-model="resultQuery" :list="list" @change="log">
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="display: flex; padding: 12px 7px !important; margin: unset; border-bottom: 1px solid #e9f5f9; " v-for="(module,index) in resultQuery" v-bind:key="module.id">

                                <div style="width: 4%; text-align: center; font-weight: bold; color: black !important;"> <img src="/drag-indicator.svg "></div>
                                <div style="width:4%; text-align:right;color:black !important; " v-if="currentPage === 1"><span>{{index+1}}</span></div>
                                <div style="width:4%; text-align:right;color:black !important; " v-else><span>{{((currentPage*10)-10) +(index+1)}}</span></div>
                                <div style="width: 15%; text-align: center; font-weight: bold; color: black !important;" v-if="isValid('CanEditInquirySetup')"> <a href="javascript:void(0)" v-on:click="Editmodule(module.id)">{{module.code}}</a></div>
                                <div style="width:15%;text-align:center;color:black !important;" v-else>{{module.code}}</div>
                                <div style="width:20%;text-align:center;color:black !important;">{{module.name}}</div>
                                <div style="width:20%;text-align:center;color:black !important;">{{module.label}}</div>
                                <div style="width:26%;text-align:center;color:black !important;">{{module.description}}</div>
                                <div style="width:10%;text-align:center;color:black !important;">
                                    <span v-if="module.isActive" class="badge badge-boxed  badge-outline-success">{{$t('InquirySetupList.Active')}}</span>
                                    <span v-else class="badge badge-boxed  badge-outline-danger">{{$t('InquirySetupList.De-Active')}}</span>
                                </div>
                            </div>
                        </draggable>

                        <!--<draggable class="row" v-model="resultQuery" style="  margin: unset;">
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="display: flex; padding: 12px 7px !important; margin: unset; border-bottom: 1px solid #e9f5f9; " v-for="(module,index) in resultQuery" v-bind:key="module.id">

                                <div style="width: 4%; text-align: center; font-weight: bold; color: black !important;"> <img src="/drag-indicator.svg "></div>
                                <div style="width:4%; text-align:right;color:black !important; " v-if="currentPage === 1"><span>{{index+1}}</span></div>
                                <div style="width:4%; text-align:right;color:black !important; " v-else><span>{{((currentPage*10)-10) +(index+1)}}</span></div>
                                <div style="width: 15%; text-align: center; font-weight: bold; color: black !important;" v-if="isValid('CanEditInquirySetup')"> <a href="javascript:void(0)" v-on:click="Editmodule(module.id)">{{module.code}}</a></div>
                                <div style="width:15%;text-align:center;color:black !important;" v-else>{{module.code}}</div>
                                <div style="width:20%;text-align:center;color:black !important;">{{module.name}}</div>
                                <div style="width:20%;text-align:center;color:black !important;">{{module.label}}</div>
                                <div style="width:26%;text-align:center;color:black !important;">{{module.description}}</div>
                                <div style="width:10%;text-align:center;color:black !important;">
                                    <span v-if="module.isActive" class="badge badge-boxed  badge-outline-success">{{$t('InquirySetupList.Active')}}</span>
                                    <span v-else class="badge badge-boxed  badge-outline-danger">{{$t('InquirySetupList.De-Active')}}</span>
                                </div>


                            </div>
                        </draggable>-->

                    </div>


                </div>
            </div>

            <add-inquiry-module-model :newmodule="newmodule"
                                      :show="show"
                                      v-if="show"
                                      @close="IsSave"
                                      :type="type" />
        </div>

    </div>
    <div v-else> <acessdenied></acessdenied></div>
</template>


<script>
    import clickMixin from '@/Mixins/clickMixin'
    import { VueDraggableNext } from 'vue-draggable-next'
    export default {
        mixins: [clickMixin],
        components: {
            draggable: VueDraggableNext,
        },
        data: function () {
            return {
                searchQuery: '',
                show: false,
                modulelist: [],
                newmodule: {
                    id: '00000000-0000-0000-0000-000000000000',
                    name: '',
                    label: '',
                    description: '',
                    code: '',
                    moduleQuestionLookUps: [],
                    isActive: true,
                    compulsory: false,
                    attachmentCompulsory: false,
                },
                type: '',
                search: '',
                currentPage: 1,
                pageCount: '',
                rowCount: '',
                arabic: '',
                english: '',

               
            }
        },
        watch: {
        },
        computed: {
            resultQuery: {
                get() {

                    return this.modulelist
                },

                set(val) {

                    this.modulelist = val;
                    var count = 1;
                    this.modulelist.forEach(function (x) {
                        x.rowNumber = count
                        count++
                    });
                    this.UpdateRow()
                }
            }
        },
        methods: {
            search22: function () {
    this.GetmoduleData(this.search, this.currentPage);
            },

            clearData: function () {
                this.search="";
                this.GetmoduleData(this.search, this.currentPage);

            },

            IsSave: function () {

                this.show = !this.show;

                this.GetmoduleData(this.search, this.currentPage);
            },
            getPage: function () {
                this.GetmoduleData(this.search, this.currentPage);
            },
            openmodel: function () {
                this.newmodule = {
                    id: '00000000-0000-0000-0000-000000000000',
                    name: '',
                    label: '',
                    code: '',
                    description: '',
                    moduleQuestionLookUps: [],
                    isActive: true,
                    compulsory: false,
                    attachmentCompulsory: false,

                }
                this.show = !this.show;
                this.type = "Add";
            },
            UpdateRow: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.post('/Project/UpdateModuleList', this.modulelist, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data.isSuccess) {
                        console.log("updated")
                    }
                    root.loading = false;
                });
            },
            GetmoduleData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Project/InquiryModuleList?pageNumber=' + this.currentPage + '&searchTerm=' + this.search, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.modulelist = response.data.results.inquiryModuleLookUp;
                        root.pageCount = response.data.pageCount;
                        root.rowCount = response.data.rowCount;
                        root.loading = false;
                    }
                    root.loading = false;
                });
            },
            Editmodule: function (Id) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Project/InquiryModuleDetail?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data) {
                            root.newmodule.id = response.data.id;
                            root.newmodule.name = response.data.name;
                            root.newmodule.label = response.data.label;
                            root.newmodule.description = response.data.description;
                            root.newmodule.code = response.data.code;
                            root.newmodule.isActive = response.data.isActive;
                            root.newmodule.compulsory = response.data.compulsory;
                            root.newmodule.attachmentCompulsory = response.data.attachmentCompulsory;
                            root.newmodule.moduleQuestionLookUps = response.data.moduleQuestionLookUps;
                            root.show = !root.show;
                            root.type = "Edit"
                        } else {
                            console.log("error: something wrong from db.");
                        }
                    },
                        function (error) {
                            this.loading = false;
                            console.log(error);
                        });

            }
        },
        created: function () {
            this.$emit('update:modelValue', this.$route.name);
        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.GetmoduleData(this.search, 1);

        }
    }
</script>