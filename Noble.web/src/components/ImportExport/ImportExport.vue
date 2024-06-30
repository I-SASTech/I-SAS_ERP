<template>
    <div class="row"  v-if="(formName=='StuffingLocation' && isValid('CanViewStuffingLocation'))
                               ||(formName=='PortOfLoading' && isValid('CanViewPartOfLoading')) ||(formName=='PortOfDestination' && isValid('CanViewPartOfDestination')) 
                               ||(formName=='OrderType' && isValid('CanViewOrderType')) ||(formName=='Service' && isValid('CanViewService')) 
                               ||(formName=='Incoterms' && isValid('CanViewIncoterms')) ||(formName=='Commodity' && isValid('CanViewCommodity')) 
                               ||(formName=='Carrier' && isValid('CanViewCarrier')) ||(formName=='ExportEXW' && isValid('CanViewExportExw')) 
                               ||(formName=='ImportFOB' && isValid('CanViewImportFob')) ||(formName=='QuantityContainer' && isValid('CanViewQuantityContainer'))">
        <div class="col-lg-12 col-sm-12 ml-auto mr-auto">
            <div class="card">
                <div class="BorderBottom ml-2 mr-2 mt-3 mb-3">
                    <span class=" DayHeading" v-if="formName=='StuffingLocation' && isValid('CanViewStuffingLocation')">{{ $t('importExport.StuffingLocation') }}</span>
                    <span class=" DayHeading" v-if="formName=='PortOfLoading' && isValid('CanViewPartOfLoading')">{{ $t('importExport.PortOfLoading') }}</span>
                    <span class=" DayHeading" v-if="formName=='PortOfDestination' && isValid('CanViewPartOfDestination')">{{ $t('importExport.PortOfDestination') }}</span>
                    <span class=" DayHeading" v-if="formName=='OrderType' && isValid('CanViewOrderType')">{{ $t('importExport.OrderType') }}</span>
                    <span class=" DayHeading" v-if="formName=='Service' && isValid('CanViewService')">{{ $t('importExport.Service') }}</span>
                    <span class=" DayHeading" v-if="formName=='Incoterms' && isValid('CanViewIncoterms')">{{ $t('importExport.Incoterms') }}</span>
                    <span class=" DayHeading" v-if="formName=='Commodity' && isValid('CanViewCommodity')">{{ $t('importExport.Commodity') }}</span>
                    <span class=" DayHeading" v-if="formName=='Carrier' && isValid('CanViewCarrier')">{{ $t('importExport.Carrier') }}</span>
                    <span class=" DayHeading" v-if="formName=='ExportEXW' && isValid('CanViewExportExw')">{{ $t('importExport.ExportEXW') }}</span>
                    <span class=" DayHeading" v-if="formName=='ImportFOB' && isValid('CanViewImportFob')">{{ $t('importExport.ImportFOB') }}</span>
                    <span class=" DayHeading" v-if="formName=='QuantityContainer' && isValid('CanViewQuantityContainer')">{{ $t('importExport.QuantityContainer') }}</span>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                            <div class="form-group">
                                <label>{{ $t('importExport.SearchbyName') }}</label>
                                <div>
                                    <input type="text" class="form-control search_input" v-model="search" name="search" id="search" :placeholder="$t('importExport.Search')" />
                                    <span class="fas fa-search search_icon"></span>
                                    <!--<importexportdropdown :formName="'StuffingLocation'"></importexportdropdown>-->
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6" v-bind:class="$i18n.locale == 'en' ? 'text-right' : 'text-left'">
                            <a href="javascript:void(0)" class="btn btn-primary" style="margin-top:27px;" v-on:click="openmodel" v-if="(formName=='StuffingLocation' && isValid('CanAddStuffingLocation'))
                               ||(formName=='PortOfLoading' && isValid('CanAddPartOfLoading')) ||(formName=='PortOfDestination' && isValid('CanAddPartOfDestination')) 
                               ||(formName=='OrderType' && isValid('CanAddOrderType')) ||(formName=='Service' && isValid('CanAddService')) 
                               ||(formName=='Incoterms' && isValid('CanAddIncoterms')) ||(formName=='Commodity' && isValid('CanAddCommodity')) 
                               ||(formName=='Carrier' && isValid('CanAddCarrier')) ||(formName=='ExportEXW' && isValid('CanAddExportExw')) 
                               ||(formName=='ImportFOB' && isValid('CanAddImportFob')) ||(formName=='QuantityContainer' && isValid('CanAddQuantityContainer'))"><i class="fa fa-plus"></i>  {{ $t('importExport.AddNew') }}</a>
                            <router-link :to="'/ImportExportSetup'">
                                <a href="javascript:void(0)" class="btn btn-outline-danger " style="margin-top:27px;">  <i class="fas fa-arrow-circle-left fa-lg"></i> </a>
                            </router-link>
                        </div>

                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                            <table class="table table-striped table-hover table_list_bg" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                <thead class="">
                                    <tr>
                                        <th>#</th>
                                       
                                        <th v-if="english=='true'" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                            {{$englishLanguage($t('importExport.ImportExportName'))  }}
                                        </th>
                                        <th v-if="isOtherLang()" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                            {{$arabicLanguage($t('importExport.ImportExportNameAr'))  }}
                                        </th>
                                        <th class="text-center" width="40%">
                                            {{ $t('importExport.Description') }}
                                        </th>
                                        <th v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                            {{ $t('importExport.Status') }}
                                        </th>


                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="(importExport,index) in importExportlist" v-bind:key="importExport.id">
                                        <td v-if="currentPage === 1">
                                            {{index+1}}
                                        </td>
                                        <td v-else>
                                            {{((currentPage*10)-10) +(index+1)}}
                                        </td>

                                        <!--<td v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'text-right'" v-if="isValid('CanEditColor')">
        <strong>
            <a href="javascript:void(0)" v-on:click="EditImportExport(importExport.id)">  {{importExport.code}}</a>
        </strong>
    </td>-->
                                        <!--<td v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'text-right'" v-else>
        {{importExport.code}}

    </td>-->
                                        <td v-if="english=='true' && (formName=='StuffingLocation' && isValid('CanEditStuffingLocation'))
                               ||(formName=='PortOfLoading' && isValid('CanEditPartOfLoading')) ||(formName=='PortOfDestination' && isValid('CanEditPartOfDestination'))
                               ||(formName=='OrderType' && isValid('CanEditOrderType')) ||(formName=='Service' && isValid('CanEditService'))
                               ||(formName=='Incoterms' && isValid('CanEditIncoterms')) ||(formName=='Commodity' && isValid('CanEditCommodity'))
                               ||(formName=='Carrier' && isValid('CanEditCarrier')) ||(formName=='ExportEXW' && isValid('CanEditExportExw'))
                               ||(formName=='ImportFOB' && isValid('CanEditImportFob')) ||(formName=='QuantityContainer' && isValid('CanEditQuantityContainer'))"
                                            v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'text-right'">
                                            <strong>
                                                <a href="javascript:void(0)" v-on:click="EditImportExport(importExport.id)">    {{importExport.name}}</a>
                                            </strong>

                                        </td>
                                        <td v-else v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'text-right'">
                                            <strong>
                                                {{importExport.name}}
                                            </strong>

                                        </td>
                                        <td v-if="arabic=='true' && (formName=='StuffingLocation' && isValid('CanEditStuffingLocation'))
                               ||(formName=='PortOfLoading' && isValid('CanEditPartOfLoading')) ||(formName=='PortOfDestination' && isValid('CanEditPartOfDestination'))
                               ||(formName=='OrderType' && isValid('CanEditOrderType')) ||(formName=='Service' && isValid('CanEditService'))
                               ||(formName=='Incoterms' && isValid('CanEditIncoterms')) ||(formName=='Commodity' && isValid('CanEditCommodity'))
                               ||(formName=='Carrier' && isValid('CanEditCarrier')) ||(formName=='ExportEXW' && isValid('CanEditExportExw'))
                               ||(formName=='ImportFOB' && isValid('CanEditImportFob')) ||(formName=='QuantityContainer' && isValid('CanEditQuantityContainer'))" v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'text-right'">
                                            <strong>
                                                <a href="javascript:void(0)" v-on:click="EditImportExport(importExport.id)">    {{importExport.nameArabic}}</a>
                                            </strong>

                                        </td>
                                        <td v-else v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'text-right'">
                                            <strong>
                                                {{importExport.nameArabic}}
                                            </strong>

                                        </td>
                                        <td class="text-center">
                                            {{importExport.description}}
                                        </td>
                                        <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">{{importExport.isActive==true?$t('importExport.Active'):$t('importExport.De-Active')}}</td>



                                    </tr>
                                </tbody>
                            </table>

                        </div>
                    </div>
                    <div class="float-left">
                        <span v-if="currentPage===1 && rowCount === 0">  {{ $t('Pagination.ShowingEntries') }}</span>
                        <span v-else-if="currentPage===1 && rowCount < 10">  {{ $t('Pagination.Showing') }} {{currentPage}}  {{ $t('Pagination.to') }} {{rowCount}}  {{ $t('Pagination.of') }} {{rowCount}}  {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage===1 && rowCount >= 11  "> {{ $t('Pagination.Showing') }} {{currentPage}} {{ $t('Pagination.to') }} {{currentPage*10}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage===1"> {{ $t('Pagination.Showing') }} {{currentPage}} {{ $t('Pagination.to') }} {{currentPage*10}} of {{rowCount}} {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage!==1 && currentPage!==pageCount"> {{ $t('Pagination.Showing') }} {{(currentPage*10)-9}} {{ $t('Pagination.to') }} {{currentPage*10}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage === pageCount"> {{ $t('Pagination.Showing') }} {{(currentPage*10)-9}} {{ $t('Pagination.to') }} {{rowCount}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                    </div>
                    <div class="float-right">
                        <div class="" v-on:click="GetImportExportData()">
                            <b-pagination pills size="sm" v-model="currentPage"
                                              :total-rows="rowCount"
                                              :per-page="10"
                                              :first-text="$t('Table.First')"
                                              :prev-text="$t('Table.Previous')"
                                              :next-text="$t('Table.Next')"
                                              :last-text="$t('Table.Last')" ></b-pagination>
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <importExportmodel :newImportExport="newImportExport"
                    :show="show"
                    :formName="formName"
                    v-if="show"
                    @close="IsSave"
                    :type="type" />

    </div>
    <div v-else> <acessdenied></acessdenied></div>

</template>

<script>
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        props: ['formName'],

        mixins: [clickMixin],
        data: function () {
            return {
                arabic: '',
                english: '',
                searchQuery: '',
                show: false,
                importExportlist: [],
                newImportExport: {
                    id: '',
                    name: '',
                    nameArabic: '',
                    description: '',
                    importExportTypes: '',
                    code: '',
                    isActive: true
                },
                type: '',
                search: '',
                currentPage: 1,
                pageCount: '',
                rowCount: '',
            }
        },
        watch: {
            search: function (val) {
                this.GetImportExportData(val, 1, this.formName);
            },
            formName: function () {
                this.GetImportExportData(this.search, 1, this.formName);
            }
        },
        methods: {
            IsSave: function () {

                this.show = false;

                this.GetImportExportData(this.search, this.currentPage);
            },
            getPage: function () {
                this.GetImportExportData(this.search, this.currentPage);
            },
            openmodel: function () {
                this.newImportExport = {
                    id: '00000000-0000-0000-0000-000000000000',
                    name: '',
                    nameArabic: '',
                    description: '',
                    importExportTypes: this.formName,
                    isActive: true

                }
                this.show = !this.show;
                this.type = "Add";
            },
            GetImportExportData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('ImportExport/ImportExportTypeList?isDropdown=false' + '&pageNumber=' + this.currentPage + '&searchTerm=' + this.search + '&importExportTypes=' + this.formName, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {

                        root.importExportlist = response.data.results;
                        root.pageCount = response.data.pageCount;
                        root.rowCount = response.data.rowCount;
                        root.loading = false;
                    }
                    root.loading = false;
                });
            },
            EditImportExport: function (Id) {


                var root = this;

                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/ImportExport/ImportExportTypeDetail?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data) {

                            root.newImportExport.id = response.data.id;
                            root.newImportExport.importExportTypes = response.data.importExportTypes;
                            root.newImportExport.name = response.data.name;
                            root.newImportExport.nameArabic = response.data.nameArabic;
                            root.newImportExport.description = response.data.description;
                            root.newImportExport.code = response.data.code;
                            root.newImportExport.isActive = response.data.isActive;
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
            this.GetImportExportData(this.search, 1);
        }
    }
</script>