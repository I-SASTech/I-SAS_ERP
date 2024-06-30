<template>
    <div class="row">
        <div class="col-lg-12 col-sm-12 ml-auto mr-auto" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">

            <div class="card">
                <div class="BorderBottom ml-2 mr-2 mt-3 mb-3">
                    <span class=" DayHeading">{{ $t('SampleRequest.SampleRequest') }}</span>
                </div>
                <div class="card-body">
                    <div class="row" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                            <div class="form-group">
                                <label>{{ $t('SampleRequest.SearchbySampleRequest') }}</label>
                                <div>
                                    <input type="text" class="form-control search_input" v-model="search" name="search" id="search" :placeholder="$t('Search')" />
                                    <span class="fas fa-search search_icon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                            <a href="javascript:void(0)" class="btn btn-primary " style="margin-top:27px;" v-on:click="AddSampleRequest"><i class="fa fa-plus"></i> {{ $t('SampleRequest.AddNew') }} </a>
                            <router-link :to="'/ProductManagement'">
                                <a href="javascript:void(0)" class="btn btn-outline-danger " style="margin-top:27px;">  <i class="fas fa-arrow-circle-left fa-lg"></i> </a>
                            </router-link>
                        </div>
                        <div class="col-lg-12">


                            <ul class="nav nav-tabs" data-tabs="tabs">
                                <li class="nav-item"><a class="nav-link" v-bind:class="{active:active == 'Draft'}" v-on:click="makeActive('Draft')" id="v-pills-home-tab" data-toggle="pill" href="#v-pills-home" role="tab" aria-controls="v-pills-home" aria-selected="true">{{ $t('SampleRequest.Draft') }}</a></li>
                                <li class="nav-item"><a class="nav-link" v-bind:class="{active:active == 'Approved'}" v-on:click="makeActive('Approved')" id="v-pills-profile-tab" data-toggle="pill" href="#v-pills-profile" role="tab" aria-controls="v-pills-profile" aria-selected="false">{{ $t('SampleRequest.Post') }}</a></li>
                                <li class="nav-item"><a class="nav-link" v-bind:class="{active:active == 'InProcess'}" v-on:click="makeActive('InProcess')" id="v-pills-profile-tab" data-toggle="pill" href="#v-pills-profile" role="tab" aria-controls="v-pills-profile" aria-selected="false">{{ $t('SampleRequest.InProcess') }}</a></li>
                                <li class="nav-item"><a class="nav-link" v-bind:class="{active:active == 'Complete'}" v-on:click="makeActive('Complete')" id="v-pills-profile-tab" data-toggle="pill" href="#v-pills-profile" role="tab" aria-controls="v-pills-profile" aria-selected="false">{{ $t('SampleRequest.Complete') }}</a></li>
                            </ul>


                            <div class="tab-content mt-3" id="nav-tabContent">
                                <div v-if="active == 'Draft'">
                                    <div class="row" v-if="selected.length > 0">
                                        <div class="col-md-3 ">
                                            <div class="dropdown">
                                                <button class="dropdown-toggle btn btn-primary  btn-block" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                    {{ $t('PurchaseOrder.BulkAction') }}
                                                </button>
                                                <div class="dropdown-menu dropdown-menu-right" aria-labelledby="dropdownMenuButton">

                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="UpdateApprovalStatus('Approved')"> {{ $t('PurchaseOrder.Approve') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="UpdateApprovalStatus('Rejected')">{{ $t('PurchaseOrder.Reject') }}</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="table-responsive">
                                        <table class="table table_list_bg">
                                            <thead class="">
                                                <tr>
                                                    <th>#</th>

                                                    <th class="text-center">
                                                        {{ $t('SampleRequest.Code') }}
                                                    </th>
                                                    <th class="text-center">
                                                        {{ $t('SampleRequest.Date') }}
                                                    </th>
                                                    <th class="text-center">
                                                        {{ $t('SampleRequest.CustomerName') }}
                                                    </th>

                                                    <th class="text-center">
                                                        {{ $t('SampleRequest.NoOfSamples') }}
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(sampleRequest,index) in sampleRequestlist" v-bind:key="sampleRequest.id">
                                                    <td v-if="currentPage === 1">
                                                        {{index+1}}
                                                    </td>
                                                    <td v-else>
                                                        {{((currentPage*10)-10) +(index+1)}}
                                                    </td>

                                                    <td class="text-center">
                                                        <strong>
                                                            <a href="javascript:void(0)" v-on:click="EditSampleRequest(sampleRequest.id)">{{sampleRequest.code}}</a>
                                                        </strong>
                                                    </td>
                                                    <td class="text-center">
                                                        {{sampleRequest.sampleDate}}
                                                    </td>
                                                    <td class="text-center">
                                                        {{sampleRequest.customerNameEn==''|| sampleRequest.customerNameEn==null?sampleRequest.customerNameAr:sampleRequest.customerNameEn}}
                                                    </td>
                                                    <td class="text-center">
                                                        {{sampleRequest.noOfSampleRequests}}
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>

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
                                        <div class="overflow-auto" v-on:click="getPage()" v-if="($i18n.locale == 'en' ||isLeftToRight())">
                                            <b-pagination pills size="lg" v-model="currentPage"
                                                          :total-rows="rowCount"
                                                          :per-page="10"
                                                          first-text="First"
                                                          prev-text="Previous"
                                                          next-text="Next"
                                                          last-text="Last"></b-pagination>
                                        </div>
                                        <div class="overflow-auto" v-on:click="getPage()" v-else>
                                            <b-pagination pills size="lg" v-model="currentPage"
                                                          :total-rows="rowCount"
                                                          :per-page="10"
                                                          first-text="الأولى"
                                                          prev-text="السابقة"
                                                          next-text="التالية"
                                                          last-text="الأخيرة"></b-pagination>
                                        </div>
                                    </div>
                                </div>
                                <div v-if="active == 'Approved'">

                                    <div class="row" v-if="selected.length > 0">
                                        <div class="col-md-3 ">
                                            <div class="dropdown">
                                                <button class="dropdown-toggle btn btn-primary  btn-block" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                    {{ $t('PurchaseOrder.BulkAction') }}
                                                </button>
                                                <div class="dropdown-menu dropdown-menu-right" aria-labelledby="dropdownMenuButton">

                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="UpdateApprovalStatus('Approved')"> {{ $t('PurchaseOrder.Approve') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="UpdateApprovalStatus('Rejected')">{{ $t('PurchaseOrder.Reject') }}</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="table-responsive">
                                        <table class="table table_list_bg">
                                            <thead class="">
                                                <tr>
                                                    <th>#</th>

                                                    <th class="text-center">
                                                        {{ $t('SampleRequest.Code') }}
                                                    </th>
                                                    <th class="text-center">
                                                        {{ $t('SampleRequest.Date') }}
                                                    </th>
                                                    <th class="text-center">
                                                        {{ $t('SampleRequest.CustomerName') }}
                                                    </th>

                                                    <th class="text-center">
                                                        {{ $t('SampleRequest.NoOfSamples') }}
                                                    </th>
                                                    <th class="text-center">
                                                        {{ $t('SampleRequest.Action') }}
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(sampleRequest,index) in sampleRequestlist" v-bind:key="sampleRequest.id">
                                                    <td v-if="currentPage === 1">
                                                        {{index+1}}
                                                    </td>
                                                    <td v-else>
                                                        {{((currentPage*10)-10) +(index+1)}}
                                                    </td>

                                                    <td class="text-center">
                                                        <strong>
                                                            <a href="javascript:void(0)" v-on:click="EditSampleRequest(sampleRequest.id)">{{sampleRequest.code}}</a>
                                                        </strong>
                                                    </td>
                                                    <td class="text-center">
                                                        {{sampleRequest.sampleDate}}
                                                    </td>
                                                    <td class="text-center">
                                                        {{sampleRequest.customerNameEn==''|| sampleRequest.customerNameEn==null?sampleRequest.customerNameAr:sampleRequest.customerNameEn}}
                                                    </td>
                                                    <td class="text-center">
                                                        {{sampleRequest.noOfSampleRequests}}
                                                    </td>

                                                    <td class="text-center">
                                                        <button class="btn btn-success  mr-1" v-on:click="ToCompleteAction(sampleRequest.id,'InProcess')">
                                                            {{ $t('SampleRequest.InProcess') }}
                                                        </button>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
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
                                        <div class="overflow-auto" v-on:click="getPage()" v-if="($i18n.locale == 'en' ||isLeftToRight())">
                                            <b-pagination pills size="lg" v-model="currentPage"
                                                          :total-rows="rowCount"
                                                          :per-page="10"
                                                          first-text="First"
                                                          prev-text="Previous"
                                                          next-text="Next"
                                                          last-text="Last"></b-pagination>
                                        </div>
                                        <div class="overflow-auto" v-on:click="getPage()" v-else>
                                            <b-pagination pills size="lg" v-model="currentPage"
                                                          :total-rows="rowCount"
                                                          :per-page="10"
                                                          first-text="الأولى"
                                                          prev-text="السابقة"
                                                          next-text="التالية"
                                                          last-text="الأخيرة"></b-pagination>
                                        </div>
                                    </div>
                                </div>
                                <div v-if="active == 'InProcess'">

                                    <div class="row" v-if="selected.length > 0">
                                        <div class="col-md-3 ">
                                            <div class="dropdown">
                                                <button class="dropdown-toggle btn btn-primary  btn-block" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                    {{ $t('PurchaseOrder.BulkAction') }}
                                                </button>
                                                <div class="dropdown-menu dropdown-menu-right" aria-labelledby="dropdownMenuButton">

                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="UpdateApprovalStatus('Approved')"> {{ $t('PurchaseOrder.Approve') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="UpdateApprovalStatus('Rejected')">{{ $t('PurchaseOrder.Reject') }}</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="table-responsive">
                                        <table class="table table_list_bg">
                                            <thead class="">
                                                <tr>
                                                    <th>#</th>

                                                    <th class="text-center">
                                                        {{ $t('SampleRequest.Code') }}
                                                    </th>
                                                    <th class="text-center">
                                                        {{ $t('SampleRequest.Date') }}
                                                    </th>
                                                    <th class="text-center">
                                                        {{ $t('SampleRequest.CustomerName') }}
                                                    </th>

                                                    <th class="text-center">
                                                        {{ $t('SampleRequest.NoOfSamples') }}
                                                    </th> 
                                                    <th class="text-center">
                                                        {{ $t('SampleRequest.Action') }}
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(sampleRequest,index) in sampleRequestlist" v-bind:key="sampleRequest.id">
                                                    <td v-if="currentPage === 1">
                                                        {{index+1}}
                                                    </td>
                                                    <td v-else>
                                                        {{((currentPage*10)-10) +(index+1)}}
                                                    </td>

                                                    <td class="text-center">
                                                        <strong>
                                                            <a href="javascript:void(0)" v-on:click="EditSampleRequest(sampleRequest.id)">{{sampleRequest.code}}</a>
                                                        </strong>
                                                    </td>
                                                    <td class="text-center">
                                                        {{sampleRequest.sampleDate}}
                                                    </td>
                                                    <td class="text-center">
                                                        {{sampleRequest.customerNameEn==''|| sampleRequest.customerNameEn==null?sampleRequest.customerNameAr:sampleRequest.customerNameEn}}
                                                    </td>
                                                    <td class="text-center">
                                                        {{sampleRequest.noOfSampleRequests}}
                                                    </td>
                                                   
                                                    <td class="text-center">
                                                        <button class="btn btn-success  mr-1" v-on:click="ToCompleteAction(sampleRequest.id,'Complete')">
                                                            {{ $t('Complete') }}
                                                        </button>
                                                    </td>





                                                </tr>
                                            </tbody>
                                        </table>

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
                                        <div class="overflow-auto" v-on:click="getPage()" v-if="($i18n.locale == 'en' ||isLeftToRight())">
                                            <b-pagination pills size="lg" v-model="currentPage"
                                                          :total-rows="rowCount"
                                                          :per-page="10"
                                                          first-text="First"
                                                          prev-text="Previous"
                                                          next-text="Next"
                                                          last-text="Last"></b-pagination>
                                        </div>
                                        <div class="overflow-auto" v-on:click="getPage()" v-else>
                                            <b-pagination pills size="lg" v-model="currentPage"
                                                          :total-rows="rowCount"
                                                          :per-page="10"
                                                          first-text="الأولى"
                                                          prev-text="السابقة"
                                                          next-text="التالية"
                                                          last-text="الأخيرة"></b-pagination>
                                        </div>
                                    </div>
                                </div>
                                <div v-if="active == 'Complete'">

                                    <div class="row" v-if="selected.length > 0">
                                        <div class="col-md-3 ">
                                            <div class="dropdown">
                                                <button class="dropdown-toggle btn btn-primary  btn-block" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                    {{ $t('PurchaseOrder.BulkAction') }}
                                                </button>
                                                <div class="dropdown-menu dropdown-menu-right" aria-labelledby="dropdownMenuButton">

                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="UpdateApprovalStatus('Approved')"> {{ $t('PurchaseOrder.Approve') }}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" v-on:click="UpdateApprovalStatus('Rejected')">{{ $t('PurchaseOrder.Reject') }}</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="table-responsive">
                                        <table class="table table_list_bg">
                                            <thead class="">
                                                <tr>
                                                    <th>#</th>
                                                    <th class="text-center">
                                                        {{ $t('SampleRequest.Code') }}
                                                    </th>
                                                    <th class="text-center">
                                                        {{ $t('SampleRequest.Date') }}
                                                    </th>
                                                    <th class="text-center">
                                                        {{ $t('SampleRequest.CustomerName') }}
                                                    </th>

                                                    <th class="text-center">
                                                        {{ $t('SampleRequest.NoOfSamples') }}
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(sampleRequest, index) in sampleRequestlist" v-bind:key="sampleRequest.id">
                                                    <td v-if="currentPage === 1">
                                                        {{index+1}}
                                                    </td>
                                                    <td v-else>
                                                        {{((currentPage*10)-10) +(index+1)}}
                                                    </td>

                                                    <td class="text-center">
                                                        <strong>
                                                            <a href="javascript:void(0)" v-on:click="EditSampleRequest(sampleRequest.id)">{{sampleRequest.code}}</a>
                                                        </strong>
                                                    </td>
                                                    <td class="text-center">
                                                        {{sampleRequest.sampleDate}}
                                                    </td>
                                                    <td class="text-center">
                                                        {{sampleRequest.customerNameEn==''|| sampleRequest.customerNameEn==null?sampleRequest.customerNameAr:sampleRequest.customerNameEn}}
                                                    </td>
                                                    <td class="text-center">
                                                        {{sampleRequest.noOfSampleRequests}}
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>

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
                                        <div class="overflow-auto" v-on:click="getPage()" v-if="($i18n.locale == 'en' ||isLeftToRight())">
                                            <b-pagination pills size="lg" v-model="currentPage"
                                                          :total-rows="rowCount"
                                                          :per-page="10"
                                                          first-text="First"
                                                          prev-text="Previous"
                                                          next-text="Next"
                                                          last-text="Last"></b-pagination>
                                        </div>
                                        <div class="overflow-auto" v-on:click="getPage()" v-else>
                                            <b-pagination pills size="lg" v-model="currentPage"
                                                          :total-rows="rowCount"
                                                          :per-page="10"
                                                          first-text="الأولى"
                                                          prev-text="السابقة"
                                                          next-text="التالية"
                                                          last-text="الأخيرة"></b-pagination>
                                        </div>
                                    </div>
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
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        data: function () {
            return {
                active: 'Draft',

                searchQuery: '',
                currency: '',
                show: false,
                sampleRequestlist: [],
               
                type: '',
                search: '',
                currentPage: 1,
                pageCount: '',
                rowCount: '',
                arabic: '',
                english: '',
                selected: [],
                selectAll: false,
                updateApprovalStatus: {
                    id: '',
                    approvalStatus: ''
                }
            }
        },
        watch: {
            search: function (val) {
                this.getData(val, 1, this.active);
            }
        },
        methods: {
           
            makeActive: function (item) {
                this.active = item;
                this.selectAll = false;
                this.selected = [];
                this.getData(this.search, 1, item);
            },
            getPage: function () {
                this.getData(this.search, this.currentPage, this.active);
            },
            AddSampleRequest: function () {
               
             //   this.$router.push('/AddSampleRequest');
                this.$router.push({
                                path: '/AddSampleRequest',
                                query: { 
                                Add: true, }
                            })
            },
            getData: function (search, currentPage, status) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                this.$https.get('/Batch/SampleRequestList?status=' + status + '&searchTerm=' + search + '&pageNumber=' + currentPage, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        
                        root.sampleRequestlist = response.data.results;
                        root.pageCount = response.data.pageCount;
                        root.rowCount = response.data.rowCount;

                    });
            },
            ToCompleteAction: function (Id,status) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Batch/ActionSampleRequestDetailQuery?Id=' + Id + '&status=' + status, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {

                        if (response.data != null) {
                            
                            root.makeActive(status)
                        }
                        else {
                            console.log("error: something wrong from db.");
                        }
                    },
                        function (error) {
                            this.loading = false;
                            console.log(error);
                        });
            },
            EditSampleRequest: function (Id) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Batch/SampleRequestDetail?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        
                        if (response.data != null) {
                            root.$store.GetEditObj(response.data);
                            root.$router.push({
                                path: '/AddSampleRequest',
                                query: { data: '',
                                Add: false, }
                            })
                        }
                        else {
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
            this.currency = localStorage.getItem('currency');

            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.makeActive("Draft");

        }
    }
</script>