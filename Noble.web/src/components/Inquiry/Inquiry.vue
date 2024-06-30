<template>
    <div v-if="isValid('CanViewInquiry')">
        <div class="row" >
            <div class="col-lg-12" v-if="isHidden">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="page-title-box">
                            <div class="row">
                                <div class="col">
                                    <h4 class="page-title">{{ $t('InquiryList.AllInquiries') }}</h4>
                                    <ol class="breadcrumb">
                                        <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('InquiryList.Home') }}</a></li>
                                        <li class="breadcrumb-item active">{{ $t('InquiryList.AllInquiries') }}</li>
                                    </ol>
                                </div>
                            
                                <div class="col-auto align-self-center">
                                    <a v-on:click="NewRequest" v-if="isValid('CanAddInquiry')" href="javascript:void(0);"
                                       class="btn btn-sm btn-outline-primary mx-1">
                                        <i class="align-self-center icon-xs ti-plus"></i>
                                        {{ $t('InquiryList.NewRequest') }}
                                    </a>
                                    <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                       class="btn btn-sm btn-outline-danger">
                                        {{ $t('Bank.Close') }}
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card">
                    <div class="card-body">
                        <div class="table-responsive">
                            <table class="table mb-0">
                                <thead class="thead-light table-hover">
                                    <tr>
                                        <th>#</th>
                                        <th>
                                            {{ $t('InquiryList.Inquiry') }} #
                                        </th>
                                        <th>
                                            {{ $t('InquiryList.CreatedOn') }}
                                        </th>
                                        <th>
                                            {{ $t('InquiryList.ReceivedBy') }}
                                        </th>
                                        <!--<th class="text-center">
                                        Process
                                    </th>-->
                                        <th class="text-center">
                                            {{ $t('InquiryList.Status') }}
                                        </th>
                                        <th class="text-center" v-if="isValid('CanViewInquiryDashboard') || isValid('CanReplyInquiryDashboard')">
                                            {{ $t('InquiryList.View') }}
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="(inquiry,index) in processlist" v-bind:key="inquiry.id">
                                        <td v-if="currentPage === 1">
                                            {{index+1}}
                                        </td>
                                        <td v-else>
                                            {{((currentPage*10)-10) +(index+1)}}
                                        </td>

                                        <td v-if="isValid('CanEditInquiry')">
                                            <strong>
                                                <a href="javascript:void(0)" v-on:click="EditInquiry(inquiry.id)">  {{inquiry.inquiryNumber}}</a>
                                            </strong>
                                        </td>
                                        <td v-else>
                                            {{inquiry.inquiryNumber}}

                                        </td>
                                        <td>
                                            {{inquiry.createdOn}}
                                        </td>
                                        <td>
                                            {{inquiry.receivedBy}}
                                        </td>
                                        <!--<td class="text-center">
                                        {{inquiry.process}}
                                    </td>-->
                                        <td class="text-center">
                                            {{inquiry.status}}
                                        </td>
                                        <td class="text-center" v-if="isValid('CanViewInquiryDashboard')|| isValid('CanReplyInquiryDashboard')">
                                            <button class="btn btn-primary btn-sm btn-icon mr-1  ml-1" v-on:click="display(inquiry.id)"><i class="fas fa-eye"></i></button>

                                        </td>
                                        <!--<td >{{color.isActive==true?$t('color.Active'):$t('color.De-Active')}}</td>-->



                                    </tr>

                                </tbody>
                            </table>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-lg-6">
                                <span v-if="currentPage === 1 && rowCount === 0">
                                    {{
                                                  $t('Pagination.ShowingEntries')
                                    }}
                                </span>
                                <span v-else-if="currentPage === 1 && rowCount < 10">
                                    {{ $t('Pagination.Showing') }}
                                    {{ currentPage }} {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }}
                                    {{ rowCount }} {{ $t('Pagination.entries') }}
                                </span>
                                <span v-else-if="currentPage === 1 && rowCount >= 11">
                                    {{ $t('Pagination.Showing') }}
                                    {{ currentPage }} {{ $t('Pagination.to') }} {{ currentPage * 10 }}
                                    {{
                                            $t('Pagination.of')
                                    }} {{ rowCount }} {{ $t('Pagination.entries') }}
                                </span>
                                <span v-else-if="currentPage === 1">
                                    {{ $t('Pagination.Showing') }} {{ currentPage }} {{
                                        $t('Pagination.to')
                                    }} {{ currentPage * 10 }} of {{ rowCount }} {{
                                              $t('Pagination.entries')
                                    }}
                                </span>
                                <span v-else-if="currentPage !== 1 && currentPage !== pageCount">
                                    {{
                                        $t('Pagination.Showing')
                                    }} {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }}
                                    {{ currentPage * 10 }} {{ $t('Pagination.of') }} {{ rowCount }} {{
                                            $t('Pagination.entries')
                                    }}
                                </span>
                                <span v-else-if="currentPage === pageCount">
                                    {{ $t('Pagination.Showing') }}
                                    {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ rowCount }} {{
                                            $t('Pagination.of')
                                    }} {{ rowCount }} {{ $t('Pagination.entries') }}
                                </span>
                            </div>
                            <div class=" col-lg-6">
                                <div class=" float-end" v-on:click="GetInquiryData()">
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

            </div>
            <div class="col-lg-12" v-else>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="page-title-box">
                            <div class="row">
                                <div class="col">
                                    <h4 class="page-title">{{ $t('InquiryList.AllInquiries') }}</h4>
                                    <ol class="breadcrumb">
                                        <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('InquiryList.Home') }}</a></li>
                                        <li class="breadcrumb-item active">{{ $t('InquiryList.AllInquiries') }}</li>
                                    </ol>
                                </div>
                                <div class="col-auto align-self-center">
                                    <a v-on:click="NewRequest" v-if="isValid('CanAddInquiry')" href="javascript:void(0);"
                                       class="btn btn-sm btn-outline-primary mx-1">
                                        <i class="align-self-center icon-xs ti-plus"></i>
                                        {{ $t('InquiryList.NewRequest') }}
                                    </a>
                                    <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                       class="btn btn-sm btn-outline-danger">
                                        {{ $t('Bank.Close') }}
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card">

                    <div class="card-body">
                        <div class="table-responsive">
                            <table class="table mb-0">
                                <thead class="thead-light table-hover">
                                    <tr>
                                        <th>#</th>
                                        <th >
                                            {{ $t('InquiryList.Inquiry') }} #
                                        </th>
                                        <th >
                                            {{ $t('InquiryList.CreatedOn') }}
                                        </th>
                                        <th >
                                            {{ $t('InquiryList.ReceivedBy') }}
                                        </th>
                                        <!--<th class="text-center">
                                        Process
                                    </th>-->
                                        <th class="text-center">
                                            {{ $t('InquiryList.Status') }}
                                        </th>
                                        <th class="text-center" v-if="isValid('CanViewInquiryDashboard') || isValid('CanReplyInquiryDashboard')">
                                            {{ $t('InquiryList.View') }}
                                        </th>


                                    </tr>
                                </thead>
                                <tbody v-bind:class="isHidden ? 'tbodyClass' : ''">
                                    <tr v-for="(inquiry,index) in processlist" v-bind:key="inquiry.id">
                                        <td v-if="currentPage === 1">
                                            {{index+1}}
                                        </td>
                                        <td v-else>
                                            {{((currentPage*10)-10) +(index+1)}}
                                        </td>

                                        <td  v-if="isValid('CanEditInquiry')">
                                            <strong>
                                                <a href="javascript:void(0)" v-on:click="EditInquiry(inquiry.id)">  {{inquiry.inquiryNumber}}</a>
                                            </strong>
                                        </td>
                                        <td  v-else>
                                            {{inquiry.inquiryNumber}}

                                        </td>
                                        <td >
                                            {{inquiry.createdOn}}
                                        </td>
                                        <td >
                                            {{inquiry.receivedBy}}
                                        </td>
                                        <!--<td class="text-center">
                                        {{inquiry.process}}
                                    </td>-->
                                        <td class="text-center">
                                            {{inquiry.status}}
                                        </td>
                                        <td class="text-center" v-if="isValid('CanViewInquiryDashboard') || isValid('CanReplyInquiryDashboard')">
                                            <button class="btn btn-primary btn-sm btn-icon mr-1  ml-1" v-on:click="display(inquiry.id)"><i class="fas fa-eye"></i></button>
                                        </td>
                                        <!--<td >{{color.isActive==true?$t('color.Active'):$t('color.De-Active')}}</td>-->


                                    </tr>

                                </tbody>
                            </table>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-lg-6">
                                <span v-if="currentPage === 1 && rowCount === 0">
                                    {{
                                                  $t('Pagination.ShowingEntries')
                                    }}
                                </span>
                                <span v-else-if="currentPage === 1 && rowCount < 10">
                                    {{ $t('Pagination.Showing') }}
                                    {{ currentPage }} {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }}
                                    {{ rowCount }} {{ $t('Pagination.entries') }}
                                </span>
                                <span v-else-if="currentPage === 1 && rowCount >= 11">
                                    {{ $t('Pagination.Showing') }}
                                    {{ currentPage }} {{ $t('Pagination.to') }} {{ currentPage * 10 }}
                                    {{
                                            $t('Pagination.of')
                                    }} {{ rowCount }} {{ $t('Pagination.entries') }}
                                </span>
                                <span v-else-if="currentPage === 1">
                                    {{ $t('Pagination.Showing') }} {{ currentPage }} {{
                                        $t('Pagination.to')
                                    }} {{ currentPage * 10 }} of {{ rowCount }} {{
                                              $t('Pagination.entries')
                                    }}
                                </span>
                                <span v-else-if="currentPage !== 1 && currentPage !== pageCount">
                                    {{
                                        $t('Pagination.Showing')
                                    }} {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }}
                                    {{ currentPage * 10 }} {{ $t('Pagination.of') }} {{ rowCount }} {{
                                            $t('Pagination.entries')
                                    }}
                                </span>
                                <span v-else-if="currentPage === pageCount">
                                    {{ $t('Pagination.Showing') }}
                                    {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ rowCount }} {{
                                            $t('Pagination.of')
                                    }} {{ rowCount }} {{ $t('Pagination.entries') }}
                                </span>
                            </div>
                            <div class=" col-lg-6">
                                <div class="float-end" v-on:click="GetInquiryData()">
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

            </div>
            <div class="col-xs-12 col-sm-12 col-lg-4 col-md-4 " v-if="isHidden" >
                <div class="card">
                    <div class="card-body inquiry-heading-section-scroolbar">
                        <div class="row">

                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-8">
                                <h6>{{ $t('InquiryList.InquiryDetails') }}-{{inquiryDetail.inquiryNumber}} </h6>
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-4">
                                <span style="background:red;font-size:15px;color:#ffffff;padding:2px 3px 2px 3px">{{inquiryDetail.timeSpan}}</span>
                            </div>

                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6">
                                <label class="invoice_lbl">{{ $t('InquiryList.Reference') }} #</label>
                                <hr style="margin-top: 0.3rem; margin-bottom: 0.1rem;" />
                                <label>{{inquiryDetail.reference}}</label>
                                <hr style="margin-top: 0.1rem;" />
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6">
                                <label class="invoice_lbl">{{ $t('InquiryList.CustomerName') }}</label>
                                <hr style="margin-top: 0.3rem; margin-bottom: 0.1rem;" />
                                <label>{{inquiryDetail.customerName}}</label>
                                <hr style="margin-top: 0.1rem;" />
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6">
                                <label class="invoice_lbl">{{ $t('InquiryList.Receiver') }}</label>
                                <hr style="margin-top: 0.3rem; margin-bottom: 0.1rem;" />
                                <label>{{inquiryDetail.receiverName}}</label>
                                <hr style="margin-top: 0.1rem;" />
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6">
                                <label class="invoice_lbl">{{ $t('InquiryList.Handler') }}</label>
                                <hr style="margin-top: 0.3rem; margin-bottom: 0.1rem;" />
                                <label>{{inquiryDetail.handlerName}}</label>
                                <hr style="margin-top: 0.1rem;" />
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6">
                                <label class="invoice_lbl">{{ $t('InquiryList.MediaType') }}</label>
                                <hr style="margin-top: 0.3rem; margin-bottom: 0.1rem;" />
                                <label>{{inquiryDetail.mediaType}}</label>
                                <hr style="margin-top: 0.1rem;" />
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6">
                                <label class="invoice_lbl">{{ $t('InquiryList.Priority') }}</label>
                                <hr style="margin-top: 0.3rem; margin-bottom: 0.1rem;" />
                                <label>{{inquiryDetail.priority}}</label>
                                <hr style="margin-top: 0.1rem;" />
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6">
                                <label class="invoice_lbl">{{ $t('InquiryList.InquiryType') }}</label>
                                <hr style="margin-top: 0.3rem; margin-bottom: 0.1rem;" />
                                <label>{{inquiryDetail.inquiryType}}</label>
                                <hr style="margin-top: 0.1rem; margin-bottom: 0.5rem;" />
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6">
                                <label class="invoice_lbl">{{ $t('InquiryList.TotalInquiries') }}</label>
                                <hr style="margin-top: 0.3rem; margin-bottom: 0.1rem;" />
                                <label>{{inquiryDetail.totalInquiry}}</label>
                                <hr style="margin-top: 0.1rem; margin-bottom: 0.5rem;" />
                            </div>

                            <!--<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                        <label class="invoice_lbl">Status</label>
                        <hr style="margin-top: 0.3rem; margin-bottom: 0.1rem;" />
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" >
                            <multiselect :options="options" v-model="value" :show-labels="false" placeholder="Select Type">

                            </multiselect>
                            <button class="btn btn-primary btn-sm mr-2 "
                                    v-on:click="UpdateStatus">
                                <span>
                                    Update Status
                                </span>

                            </button>
                        </div>-->
                            <!--<hr style="margin-top: 0.3rem; margin-bottom: 0.1rem;" />
                        <label>{{inquiryDetail.inquiryType}}</label>-->
                            <!--<hr style="margin-top: 0.1rem;" />
                        </div>-->
                        </div>
                    </div>
                </div>
            </div>
            <colormodel :newcolor="newColor"
                        :show="show"
                        v-if="show"
                        @close="IsSave"
                        :type="type" />
        </div>
        <div class="row mt-1" v-if="isHidden" :key="inquiryDetailRander" >
            <div class="col-lg-12 col-sm-12 col-xs-12 ">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="row mb-5">
                                    <div class="col-lg-6 ">
                                        <span style="font-size:20px;" class="fw-bold">{{inquiryDetail.inquiryNumber}}-<span style="font-size:15px;">{{getDate(inquiryDetail.dateTime)}}</span></span>
                                    </div>
                                    <div class="col-lg-6  pt-2" >
                                        <span>{{currentDate}}</span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12 comment-section-scroolbar">
                                <div>
                                    <!--inquiryModuleLookUp-->
                                    <ul class="nav nav-tabs" data-tabs="tabs">
                                        <li class="nav-item">
                                            <a class="nav-link" v-bind:class="{active:activeInquiry == 'General'}" v-on:click="makeActiveInquiry('General')" id="v-pills-home-tab" data-toggle="pill" href="#v-pills-home" role="tab" aria-controls="v-pills-home" aria-selected="true">{{ $t('InquiryList.GDescription') }}</a>
                                        </li>
                                        <li class="nav-item" v-for="module in inquiryDetail.inquiryModuleLookUp" :key="module.id">
                                            <a class="nav-link" v-bind:class="{active:activeInquiry == module.name}" v-on:click="makeActiveInquiry(module.name)" id="v-pills-home-tab" data-toggle="pill" href="#v-pills-home" role="tab" aria-controls="v-pills-home" aria-selected="true">{{module.name}}</a>
                                        </li>

                                    </ul>


                                </div>
                                <div class="tab-content mt-3 " id="nav-tabContent">
                                    <div v-if="activeInquiry =='General'">

                                        <div class="row ml-2">
                                            <div class="col-lg-12 ">

                                                <span style="font-size:17px; font-weight:bold"> {{ $t('InquiryList.GeneralDescription') }}:</span><br />
                                                <!--<textarea class="form-control" />-->
                                                <span style="font-size:16px;">{{inquiryDetail.description}}</span>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="tab-content mt-3 " id="nav-tabContent" v-for="module in inquiryDetail.inquiryModuleLookUp" :key="module.id">

                                    <div v-if="activeInquiry ==module.name">
                                        <div class="row ml-2" v-if="module.moduleQuestionLookUps.length > 0">
                                            <div class="col-lg-6 " v-for="(ques,index) in module.moduleQuestionLookUps" :key="ques.id">

                                                <span style="font-size:17px; font-weight:bold">Q{{index+1}}: {{ques.question}}:</span><br />
                                                <!--<textarea class="form-control" />-->
                                                <span style="font-size:16px;">{{ques.answer}}</span>
                                            </div>
                                            <div class="col-12 ">
                                                <button class="btn btn-primary  mt-4"
                                                        v-on:click="AttachmentModule(module.attachments)">
                                                    <span>
                                                        {{ $t('InquiryList.Attachment') }}
                                                    </span>

                                                </button>
                                            </div>
                                        </div>
                                        <div class="row ml-2" v-else>
                                            <div class="col-lg-12 ">
                                                <span style="font-size:17px; font-weight:bold">{{ $t('InquiryList.Description') }}:</span><br />

                                                <!--<textarea class="form-control" />-->
                                                <span style="font-size:16px;">{{module.description}}</span>
                                            </div>
                                            <div class="col-12 ">
                                                <button class="btn btn-primary  mt-4"
                                                        v-on:click="AttachmentModule(module.attachments)">
                                                    <span>
                                                        {{ $t('InquiryList.Attachment') }}
                                                    </span>

                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-12">
                                        <hr style="margin-top: 0.3rem; margin-bottom: 0.1rem;" />
                                    </div>
                                    <div class="col-lg-6 ">

                                        <span style="font-size:15px;font-weight:bold;">{{ $t('InquiryList.TermAndCondition') }}:</span><br />
                                        <!--<textarea class="form-control" />-->
                                        <span style="font-size:16px;">{{inquiryDetail.termAndCondition}}</span>
                                    </div>
                                    <div class="col-lg-6 ">

                                        <span style="font-size:15px;font-weight:bold;">{{ $t('InquiryList.UserMessage') }}:</span><br />
                                        <!--<textarea class="form-control" />-->
                                        <span style="font-size:16px;">{{inquiryDetail.userMessage}}</span>
                                    </div>
                                    <div class="col-12 ">
                                        <button class="btn btn-primary btn-sm mt-4"
                                                v-on:click="AttachmentModule(inquiryDetail.attachmentList)">
                                            <span>
                                                {{ $t('InquiryList.Attachment') }}
                                            </span>

                                        </button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
        <div class="row mt-1" v-if="isHidden" >
            <div class="col-lg-8 col-sm-8 col-xs-12 ">
                <div class="card">
                    <div class="card-body">
                        <div class="row">

                            <div class="col-lg-12">
                                <div>
                                    <ul class="nav nav-tabs" data-tabs="tabs">
                                        <li class="nav-item"><a class="nav-link" v-bind:class="{active:active == 'Comments'}" v-on:click="makeActive('Comments')" id="v-pills-home-tab" data-toggle="pill" href="#v-pills-home" role="tab" aria-controls="v-pills-home" aria-selected="true">{{ $t('InquiryList.CommentsDiscussion') }}</a></li>
                                        <!--<li class="nav-item"><a class="nav-link" v-bind:class="{active:active == 'Discussions'}" v-on:click="makeActive('Discussions')" id="v-pills-home-tab" data-toggle="pill" href="#v-pills-home" role="tab" aria-controls="v-pills-home" aria-selected="true">Discussions</a></li>-->
                                        <li class="nav-item"><a class="nav-link" v-bind:class="{active:active == 'Meetings'}" v-on:click="makeActive('Meetings')" id="v-pills-profile-tab" data-toggle="pill" href="#v-pills-profile" role="tab" aria-controls="v-pills-profile" aria-selected="false">{{ $t('InquiryList.Meetings') }}</a></li>
                                        <li class="nav-item"><a class="nav-link" v-bind:class="{active:active == 'Email'}" v-on:click="makeActive('Email')" id="v-pills-profile-tab" data-toggle="pill" href="#v-pills-profile" role="tab" aria-controls="v-pills-profile" aria-selected="false">{{ $t('InquiryList.Email') }}</a></li>

                                    </ul>
                                </div>
                                <div class="tab-content mt-3 " id="nav-tabContent">
                                    <div v-if="active == 'Comments'" class=" comment-section-scroolbar">
                                        <div class="row pl-4 pr-4 pt-2 pb-2" v-for="c in commentList" :key="c.id">
                                            <div class="col-lg-10" v-if="c.replyCommentedId == null">
                                                <button class="button button5">{{c.name[0].toUpperCase()}}</button>
                                                <span style="margin-left:10px;font-size:16px;font-weight:500">{{c.name}}</span>
                                                <br v-if="$i18n.locale == 'ar'" />

                                                <span style="margin-left: 5px; font-size: 10px; font-weight: 400; color: #67727E; ">{{c.timeSpan}}</span>
                                            </div>
                                            <div class="col-lg-2 " v-if="c.replyCommentedId == null" >
                                                <img src="/images/reply.png" />
                                                <span><a href="javascript:void(0)" style="color: #2F80ED;padding-left:3px;" v-on:click="isReplyOnComment(c)">{{ $t('InquiryList.Reply') }}</a></span>
                                            </div>
                                            <div class="col-lg-12 mt-1" v-if="c.replyCommentedId == null">
                                                <p style="color: #67727E;margin:0px;">{{c.comment}}</p>
                                                <a href="javascript:void(0)" v-if="c.commentedData.length >0" v-on:click="c.isViewReply = !c.isViewReply">{{ $t('InquiryList.ViewAll') }} {{c.commentedData.length}} {{ $t('InquiryList.Replies') }} </a>
                                            </div>
                                            <div class="col-12" v-if="c.isViewReply">
                                                <div class="row" v-for="k in c.commentedData" :key="k.id">
                                                    <div class="col-1">

                                                    </div>
                                                    <div class="col-11">
                                                        <button class="button button5">{{k.name[0].toUpperCase()}}</button>
                                                        <span style="margin-left:10px;font-size:16px;font-weight:500">{{k.name}}</span>
                                                        <br v-if="$i18n.locale == 'ar'" />
                                                        <span style="margin-left: 5px; font-size: 10px; font-weight: 400; color: #67727E; ">{{k.timeSpan}}</span>
                                                        <p style="color: #67727E; margin:0px;">{{k.comment}}</p>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12" v-if="c.isReply && c.replyCommentedId == null">
                                                <div class="row" v-if="isValid('CanReplyInquiryDashboard')">
                                                    <div class="col-lg-2 " >
                                                        <button class="button button5">{{comment.name[0].toUpperCase()}}</button>
                                                    </div>
                                                    <div class="col-lg-10">
                                                        <textarea class="form-control pl-2" :placeholder="$t('InquiryList.Addacomment')" rows="4" style="border-radius:5px;" v-model="comment.comment"></textarea>
                                                    </div>
                                                    <div class="col-lg-12  mt-3 mr-0 " >
                                                        <a style="background-color:#5491FF; color: #fff; padding: 10px 20px; border-radius:3px;" v-on:click="SaveComment">{{ $t('InquiryList.Save') }}</a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row pl-4 pr-4 pb-4" v-if="isSimpleComment && isValid('CanReplyInquiryDashboard')">
                                            <div class="col-lg-1">
                                                <button class="button button5">{{comment.name[0].toUpperCase()}}</button>
                                            </div>
                                            <div class="col-lg-11">
                                                <textarea class="form-control pl-2" :placeholder="$t('InquiryList.Addacomment')" rows="4" style="border-radius:5px;" v-model="comment.comment"></textarea>
                                            </div>
                                        </div>
                                        <div class="row mt-3 mb-3" v-if="isSimpleComment && isValid('CanReplyInquiryDashboard')" >
                                            <div class="col-lg-12 ">
                                                <a style="background-color:#5491FF; color: #fff; padding: 10px 20px; border-radius:3px;" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'me-4' : 'ms-4'" v-on:click="SaveComment">{{ $t('InquiryList.Save') }}</a>
                                            </div>
                                        </div>
                                    </div>
                                    <div v-if="active == 'Meetings'" :key="meetingRander" class=" comment-section-scroolbar">
                                        <div class="row pl-4 pr-4 pt-2 pb-2" v-for="meeting in meetingList" :key="meeting.id">
                                            <div class="col-lg-9">
                                                <h6>{{meeting.agenda}}</h6>
                                                <a href="javascript:void(0)" v-on:click="meeting.viewAllAttendant = !meeting.viewAllAttendant">{{ $t('InquiryList.ViewAttendant') }}</a>
                                            </div>
                                            <div class="col-lg-3 " >
                                                <span>{{getDate(meeting.date)}}</span>

                                            </div>
                                            <div v-if="meeting.viewAllAttendant" class="pl-3 pr-4 pt-2 pb-2">
                                                <div v-for="attendant in meeting.inquiryMeetingAttendantLookUp" :key="attendant.id">
                                                    <a href="javascript:void(0)" v-if="attendant.employeeName!= '' || attendant.employeeName!= null ">{{attendant.employeeName}}</a>
                                                    <a href="javascript:void(0)" v-else>{{attendant.employeeName}}</a>
                                                </div>
                                            </div>

                                            <div class="col-lg-12 m-0">
                                                <p style="color: #67727E;">{{meeting.description}}</p>
                                            </div>
                                        </div>

                                        <div class="row pl-4 pr-4 pt-2 pb-2" v-if="isValid('CanReplyInquiryDashboard')">
                                            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6" >
                                                <label>{{ $t('InquiryList.MeetingAgenda') }}: <span class="text-danger"> *</span></label>
                                                <input  v-model="meeting.agenda" class="form-control" />

                                            </div>
                                            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6" >
                                                <label>{{ $t('InquiryList.MeetingDate') }}: <span class="text-danger"> *</span></label>
                                                <datepicker v-model="meeting.date" />

                                            </div>
                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" >
                                                <label>{{ $t('InquiryList.MeetingAttendant') }}: <span class="text-danger"> *</span></label>
                                                <employeeDropdown :isMultiple="true" @update:modelValue="GetAttendantList" />

                                            </div>
                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" >
                                                <label>{{ $t('InquiryList.Description') }}: <span class="text-danger"> *</span></label>
                                                <textarea class="form-control pl-2" :placeholder="$t('InquiryList.AddaDescription')" v-model="meeting.description" rows="4" style="border-radius:5px;"></textarea>

                                            </div>
                                            <div class="col-lg-12  mt-3 mr-0 " >
                                                <a style="background-color:#5491FF; color: #fff; padding: 10px 20px; border-radius:3px;" v-on:click="SaveMeeting">{{ $t('InquiryList.Save') }}</a>
                                            </div>


                                        </div>
                                    </div>
                                    <div v-if="active == 'Email'" :key="emailRander" class=" comment-section-scroolbar">
                                        <div class="col-lg-12  m-0"  v-if="isValid('CanReplyInquiryDashboard')">
                                            <button class="btn btn-primary m-0" v-on:click="sendEmail" @update:modelValue="SendEmailData">{{ $t('InquiryList.SendNewEmail') }}</button>
                                            <button class="btn btn-primary ms-2 " v-on:click="receivedEmail" @update:modelValue="ReceiveEmailData">{{ $t('InquiryList.ReceivedEmail') }}</button>
                                        </div>
                                        <div class="row pl-4 pr-4 pt-2 pb-2" v-for="email in emailList" :key="email.id">


                                            <div class="col-lg-9 mt-1">
                                                <span style="font-size:15px;font-weight:bold;">{{email.subject}}<span style="font-size:10px;" v-if="email.isReceived">({{ $t('InquiryList.Received') }})</span></span>
                                                <!--<span><a href="javascript:void(0)" style="color: #2F80ED;padding-left:3px;">Email From</a></span>-->
                                            </div>

                                            <div class="col-lg-3  mt-1" >
                                                <span style="font-size:15px;">{{email.dateTime}}</span>
                                            </div>
                                            <div class="col-lg-12">
                                                <span><a href="javascript:void(0)" style="color: #2F80ED;padding-left:3px;" v-on:click="email.isViewEmail =!email.isViewEmail">{{ $t('InquiryList.ViewAllEmail') }}</a></span>
                                            </div>
                                            <div v-if="email.isViewEmail" class="pl-3 pr-4 pt-2 pb-2">
                                                <div v-for="emailed in email.inquiryEmailCcDetails" :key="emailed.id">
                                                    <a href="javascript:void(0)">{{emailed.email}}</a>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 m-0">
                                                <p style="color: #67727E; margin:0">{{email.description}}</p>
                                            </div>
                                        </div>



                                    </div>
                                </div>


                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-xs-12 col-sm-4 col-lg-4 col-md-4">
                <div class="card">
                    <div class="card-body status-history-section-scroolbar">
                        <div class="row">
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6">
                                <h5>{{ $t('InquiryList.StatusHistory') }}</h5>
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-6"  v-if="isValid('CanReplyInquiryDashboard')">
                                <a href="javascript:void(0)" class="btn btn-primary btn-sm mt-1" v-on:click="UpdateStatus">{{ $t('InquiryList.UpdateStatus') }} </a>

                            </div>
                        </div>
                        <div>
                            <table class="table mb-0">
                                <thead class="thead-light table-hover">
                                    <tr>
                                        <!--<th>#</th>-->
                                        <th >
                                            {{ $t('InquiryList.Status') }}
                                        </th>
                                        <th >
                                            {{ $t('InquiryList.Date') }}
                                        </th>
                                        <th >
                                            {{ $t('InquiryList.Description') }}
                                        </th>


                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="(status) in inquiryDetail.inquiryStatusLookUp" v-bind:key="status.id">
                                        <!--<td>
                                        {{index+1}}
                                    </td>-->

                                        <td >
                                            {{status.status}}
                                        </td>

                                        <td >
                                            {{status.dateTime}}
                                        </td>
                                        <td >

                                            {{status.reason}}

                                        </td>

                                        <!--<td >{{color.isActive==true?$t('color.Active'):$t('color.De-Active')}}</td>-->



                                    </tr>
                                </tbody>
                            </table>
                        </div>


                    </div>
                </div>
            </div>
        </div>
             <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        <bulk-attachment :attachmentList="selectedModuleAttachments" :show="showModuleAttachment" v-if="showModuleAttachment" @close="showModuleAttachment = false" />

        <email-compose :show="emailComposeShow" v-if="emailComposeShow" @close="emailComposeShow = false" :headerFooter="headerFooter" @update:modelValue="SendEmailData" :formName="'SimpleEmail'"></email-compose>
        <email-compose-received :show="emailComposeReceivedShow" v-if="emailComposeReceivedShow" @close="emailComposeReceivedShow = false" @update:modelValue="ReceiveEmailData" :formName="'SimpleEmail'"></email-compose-received>
        <inquiry-status-update :inquiryId="comment.inquiryId" :status="lastStaus" :show="statusShow" @update:modelValue="UpdateStatusList" v-if="statusShow" @close="statusShow = false"></inquiry-status-update>
    </div>

        <div v-else>
        <acessdenied></acessdenied>
    </div>

</template>

<script>
 import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    import clickMixin from '@/Mixins/clickMixin'
    /* import Multiselect from 'vue-multiselect'*/
    import moment from "moment";
    export default {
        mixins: [clickMixin],
        components: {
                Loading
            /* Multiselect*/
        },
        data: function () {
            return {
                  loading: false,
                lastStaus: '',
                statusShow: false,
                selectedModuleAttachments: [],
                showModuleAttachment: false,
                active: 'Comments',
                activeInquiry: 'General',
                isSimpleComment: true,
                emailComposeShow: false,
                emailComposeReceivedShow: false,
                isHidden: false,
                arabic: '',
                english: '',
                searchQuery: '',
                newCC: '',
                show: false,
                processlist: [],
                ccOptions: [],
                ccValue: [],
                currentDate: '',
                meetingRander: 0,
                emailRander: 0,
                inquiryDetailRander: 0,

                options: ['Pending', 'Completed', 'Overdue', 'Closed'],
                value: '',
                search: '',
                currentPage: 1,
                pageCount: '',
                rowCount: '',
                processId: '',
                inquiryDetail: '',
                headerFooter: {
                    footerEn: '',
                    footerAr: '',
                    company: ''
                },
                emailCompose: '',
                comment: {
                    id: '00000000-0000-0000-0000-000000000000',
                    name: '',
                    comment: '',
                    replyCommentedId: '',
                    dateTime: '',
                    inquiryId: ''
                },
                meeting: {
                    id: '00000000-0000-0000-0000-000000000000',
                    agenda: '',
                    date: '',
                    description: '',
                    inquiryId: '',
                    inquiryMeetingAttendantLookUp: []
                },
                email: {
                    id: '00000000-0000-0000-0000-000000000000',
                    subject: '',
                    description: '',
                    isReceived: '',
                    inquiryId: '',
                    inquiryEmailCcDetails: []
                },
                commentList: [],
                meetingList: [],
                emailList: []
            }
        },
        watch: {
            search: function (val) {
                this.GetInquiryData(val, 1);
            }
        },
        methods: {
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },
            UpdateStatusList: function (data) {
                this.inquiryDetail.inquiryStatusLookUp.push({
                    status: data.status,
                    reason: data.reason,
                    dateTime: data.dateTime,
                    inquiryId: data.inquiryId
                })
                this.statusShow = false
            },
            UpdateStatus: function () {
                this.lastStaus = this.inquiryDetail.inquiryStatus;
                this.statusShow = !this.statusShow;
            },
            AttachmentModule: function (attachment) {
                this.selectedModuleAttachments = attachment
                this.showModuleAttachment = true;
            },
            ReceiveEmailData: function (emailData) {
                this.email.subject = emailData.subject;
                this.email.description = emailData.description;
                this.email.isReceived = true;
                var root = this;
                emailData.EmailTo.forEach(function (x) {
                    root.email.inquiryEmailCcDetails.push({
                        email: x.cc,
                        id: '00000000-0000-0000-0000-000000000000',
                        inquiryEmailId: '00000000-0000-0000-0000-000000000000',
                    })
                });
                this.emailComposeReceivedShow = false
                this.SaveEmail()
            },
            SendEmailData: function (emailData) {
                this.email.subject = emailData.subject;
                this.email.description = emailData.description;
                this.email.isReceived = false;
                var root = this;
                emailData.EmailTo.forEach(function (x) {
                    root.email.inquiryEmailCcDetails.push({
                        email: x.cc,
                        id: '00000000-0000-0000-0000-000000000000',
                        inquiryEmailId: '00000000-0000-0000-0000-000000000000',
                    })
                });
                this.emailComposeShow = false
                this.SaveEmail()
            },
            getDate: function (date) {
                return moment(date).format('yyyy-MM-DD')
            },
            GetAttendantList: function (attendant) {
                this.meeting.inquiryMeetingAttendantLookUp = []
                var root = this;
                if (attendant != null || attendant != undefined) {
                    attendant.forEach(function (x) {
                        root.meeting.inquiryMeetingAttendantLookUp.push({
                            employeeId: x.id,
                            meetingId: '00000000-0000-0000-0000-000000000000',
                            id: '00000000-0000-0000-0000-000000000000'
                        })
                    })
                }
            },
            isReplyOnComment: function (data) {
                this.isSimpleComment = !this.isSimpleComment;
                data.isReply = !data.isReply
                if (data.isReply)
                    this.comment.replyCommentedId = data.id
                else
                    this.comment.replyCommentedId = ''
                this.comment.comment = ''
            },
            //GetEmailData: function (data) {
            //    this.emailCompose = data
            //    console.log(data)
            //    console.log(this.emailCompose)
            //},
            sendEmail: function () {
                this.emailComposeShow = true;
            },
            receivedEmail: function () {
                this.emailComposeReceivedShow = true;
            },
            AddNewCC: function (newEmail) {
                const email = {
                    name: newEmail,
                    code: newEmail.substring(0, 2) + Math.floor((Math.random() * 10000000))
                }
                this.ccOptions.push(email)
                this.ccValue.push(email)
                //this.inquiry.mediaType = this.newCC
            },
            makeActive: function (item) {


                this.active = item;

            },
            makeActiveInquiry: function (item) {


                this.activeInquiry = item;

            },
            display: function (id) {
                this.comment.inquiryId = id
                this.meeting.inquiryId = id
                this.email.inquiryId = id
                this.inquiryDetailRander++;
                this.GetListComment();
                this.GetListMeeting();
                this.GetListEmail();
                this.GetInquiryDataById(id)
                this.isHidden = true
            },
            NewRequest: function () {
                this.$router.push('/AddInquiry');
            },
            IsSave: function () {

                this.show = false;

                this.GetInquiryData(this.search, this.currentPage);
            },
            getPage: function () {
                this.GetInquiryData(this.search, this.currentPage);
            },

            GetInquiryData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Project/GetInquiryList?processId=' + this.processId + '&pageNumber=' + this.currentPage + '&branchId=' + localStorage.getItem('BranchId'), { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.processlist = response.data.results;
                        root.pageCount = response.data.pageCount;
                        root.rowCount = response.data.rowCount;
                        root.loading = false;
                    }
                    root.loading = false;
                });
            },
            EditInquiry: function (Id) {


                var root = this;

                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Project/GetInquiryDetail?id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data) {
                              root.$store.GetEditObj(response.data);
                            root.$router.push({
                                path: '/AddInquiry',
                                query: { 
                                    data: '',
                                    Add: false,
                                    }
                            })

                        } else {
                            console.log("error: something wrong from db.");
                        }
                    },
                        function (error) {
                            this.loading = false;
                            console.log(error);
                        });

            },
            GetInquiryDataById: function (id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get('/Project/GetInquiryDetail?id=' + id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {


                        root.inquiryDetail = response.data;
                        root.value = response.data.status
                        root.loading = false;
                    }
                    root.loading = false;
                });
            },
            GetHeaderDetail: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get("/Company/GetCompanyDetail?id=" + localStorage.getItem('CompanyID'), { headers: { Authorization: `Bearer ${token}` }, })
                    .then(function (response) {
                        if (response.data != null) {
                            root.headerFooter.company = response.data;



                            if (root.overWrite) {

                                root.headerFooter.company.nameArabic = root.businessNameArabic;
                                root.headerFooter.company.nameEnglish = root.businessNameEnglish;
                                root.headerFooter.company.categoryArabic = root.businessTypeArabic;
                                root.headerFooter.company.categoryEnglish = root.businessTypeEnglish;

                                root.headerFooter.company.companyNameArabic = root.companyNameArabic;
                                root.headerFooter.company.companyNameEnglish = root.companyNameEnglish;
                                root.headerFooter.company.logoPath = root.businessLogo;

                            }

                        }
                    });
            },

            SaveComment: function () {
                   this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                this.comment.dateTime = moment().format('lll');
                this.$https.post('/Project/SaveInquiryComment', this.comment, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {

                        if (response.data.isSuccess == true) {
                            root.comment.comment = ''
                            root.comment.replyCommentedId = ''
                            root.commentList = response.data.inquiryCommentList.results.inquiryCommentLookUpModels;
                            root.isSimpleComment = true;
                        }

                    })
                    .catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                text: error.response.data,

                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false);
            },

            SaveMeeting: function () {
                   this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.post('/Project/SaveInquiryMeeting', this.meeting, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {

                        if (response.data.isSuccess == true) {
                            root.meeting.agenda = ''
                            root.meeting.description = ''
                            root.meeting.date = ''
                            root.meeting.inquiryMeetingAttendantLookUp = []
                            root.meetingRander++
                            root.meetingList = response.data.inquiryMeetingList.results.inquiryMeetingLookUpModels;

                        }

                    })
                    .catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                text: error.response.data,

                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false);
            },
            SaveEmail: function () {
                   this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.post('/Project/SaveInquiryEmail', this.email, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {

                        if (response.data.isSuccess == true) {

                            root.emailRander++
                            root.emailList = response.data.inquiryEmailList.results.inquiryEmailLookUpModels;

                        }

                    })
                    .catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                text: error.response.data,

                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false);
            },

            GetListComment: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.commentList = []
                this.$https.get('/Project/InquiryCommentList?inquiryId=' + this.comment.inquiryId, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {

                        if (response.data.results.inquiryCommentLookUpModels.length > 0) {
                            root.comment.comment = ''
                            root.commentList = response.data.results.inquiryCommentLookUpModels;

                        }

                    })
                    .catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                text: error.response.data,

                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false);
            },

            GetListMeeting: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.meetingList = []
                this.$https.get('/Project/InquiryMeetingList?inquiryId=' + this.meeting.inquiryId, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {

                        if (response.data.results.inquiryMeetingLookUpModels.length > 0) {

                            root.meetingList = response.data.results.inquiryMeetingLookUpModels;

                        }

                    })
                    .catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                text: error.response.data,

                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false);
            },

            GetListEmail: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.emailList = []
                this.$https.get('/Project/InquiryEmailList?inquiryId=' + this.email.inquiryId, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {

                        if (response.data.results.inquiryEmailLookUpModels.length > 0) {

                            root.emailList = response.data.results.inquiryEmailLookUpModels;

                        }

                    })
                    .catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                text: error.response.data,

                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false);
            }
        },
        created: function () {
            this.GetHeaderDetail()
            this.$emit('update:modelValue', this.$route.name);
        },
        mounted: function () {

            this.comment.name = localStorage.getItem('LoginUserName');
            this.comment.dateTime = moment().format('lll');
            this.currentDate = moment().format('yyyy MMMM DD');
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.GetInquiryData(this.search, 1);
        }
    }
</script>

<style>
    .button {
        background-color: #04AA6D;
        border: none;
        color: white;
        padding: 2px 10px 2px 10px;
        text-align: center;
        text-decoration: none;
        display: inline-block;
        font-size: 16px;
    }

    .button5 {
        border-radius: 50%;
    }


    .my-custom-scrollbar {
        position: relative;
        height: 160px;
        overflow: auto;
    }

    .comment-section-scroolbar {
        position: relative;
        height: 300px;
        overflow-x: hidden;
        overflow-y: auto;
    }

    .status-history-section-scroolbar {
        position: relative;
        height: 380px;
        overflow-x: hidden;
        overflow-y: auto;
    }

    .inquiry-heading-section-scroolbar {
        position: relative;
        height: 350px;
        overflow-x: hidden;
        overflow-y: auto;
    }

    .table-wrapper-scroll-y {
        display: block;
    }
</style>
