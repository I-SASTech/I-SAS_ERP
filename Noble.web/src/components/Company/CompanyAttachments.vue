<template>
    <div class="col-lg-10 ml-auto mr-auto " v-if="$route.query.data==undefined">
        <div class="card">
            <div class="card-header">
                <div class="ml-2 mt-2 col-sm- float-left">
                   <h5 class="card-title DayHeading "> Attachments</h5>
                </div>
                <div class="col-sm-6 float-right">
                    <a href="javascript:void(0)" class="btn btn-outline-primary  float-right" v-on:click="show=!show"><i class="fa fa-upload"></i> Upload</a>
                    <router-link :to="'/Miscellaneous'">
                        <a href="javascript:void(0)" class="btn btn-outline-primary  float-right"><i class="fas fa-arrow-circle-left fa-lg"></i> </a>
                    </router-link>
                </div>
            </div>

            <div class="card-body">
                <div class=" table-responsive">
                    <table class="table ">
                        <thead class="m-0">
                            <tr>
                                <th>#</th>
                                <th>Date</th>
                                <th>Logo </th>
                                <th>Commercial Registration</th>
                                <th>Business Licence</th>
                                <th>Civil Defence License</th>
                                <th>CCTV Licence</th>
                            </tr>
                        </thead>
                        <tbody v-if="company.companyAttachments.length > 0">
                            <tr v-for="(attachment,index) in company.companyAttachments" v-bind:key="index">
                                <td v-if="company.companyAttachments[0].date != '' && company.companyAttachments[0].date != null">
                                    {{index+1}}
                                </td>
                                <th>
                                    <span v-if="attachment.date != null">
                                        {{$formatDate(company.date)   }}
                                    </span>
                                </th>
                                <td>
                                    <button class="btn btn-primary  btn-icon mr-2"
                                            v-if="attachment.logo != ''"
                                            v-on:click="DownloadAttachment(attachment.logo)">
                                        <i class="fa fa-download"></i>
                                    </button>
                                </td>
                                <td>
                                    <button class="btn btn-primary  btn-icon mr-2"
                                            v-if="attachment.commercialRegistration != ''"
                                            v-on:click="DownloadAttachment(attachment.commercialRegistration)">
                                        <i class="fa fa-download"></i>
                                    </button>
                                </td>
                                <td>
                                    <button class="btn btn-primary  btn-icon mr-2"
                                            v-if="attachment.businessLicence != ''"
                                            v-on:click="DownloadAttachment(attachment.businessLicence)">
                                        <i class="fa fa-download"></i>
                                    </button>
                                </td>
                                <td>
                                    <button class="btn btn-primary  btn-icon mr-2"
                                            v-if="attachment.civilDefenceLicense != ''"
                                            v-on:click="DownloadAttachment(attachment.civilDefenceLicense)">
                                        <i class="fa fa-download"></i>
                                    </button>
                                </td>
                                <td>
                                    <button class="btn btn-primary  btn-icon mr-2"
                                            v-if="attachment.cctvLicence != ''"
                                            v-on:click="DownloadAttachment(attachment.cctvLicence)">
                                        <i class="fa fa-download"></i>
                                    </button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>

            <div class="card-footer">
                <div class="float-left">
                    <span v-if="currentPage===1 && rowCount === 0">  {{ $t('Pagination.ShowingEntries') }}</span>
                    <span v-else-if="currentPage===1 && rowCount < 10">  {{ $t('Pagination.Showing') }} {{currentPage}}  {{ $t('Pagination.to') }} {{rowCount}}  {{ $t('Pagination.of') }} {{rowCount}}  {{ $t('Pagination.entries') }}</span>
                    <span v-else-if="currentPage===1 && rowCount >= 11  "> {{ $t('Pagination.Showing') }} {{currentPage}} {{ $t('Pagination.to') }} {{currentPage*10}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                    <span v-else-if="currentPage===1"> {{ $t('Pagination.Showing') }} {{currentPage}} {{ $t('Pagination.to') }} {{currentPage*10}} of {{rowCount}} {{ $t('Pagination.entries') }}</span>
                    <span v-else-if="currentPage!==1 && currentPage!==pageCount"> {{ $t('Pagination.Showing') }} {{(currentPage*10)-9}} {{ $t('Pagination.to') }} {{currentPage*10}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                    <span v-else-if="currentPage === pageCount"> {{ $t('Pagination.Showing') }} {{(currentPage*10)-9}} {{ $t('Pagination.to') }} {{rowCount}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                </div>
                <div class="float-right">
                    <div class="" v-on:click="GetCompanyData(search, currentPage)">
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

        <company-attachment-modal :show="show"
                                  v-if="show"
                                  @close="closeModal" />
    </div>
</template>
<script>
    import moment from "moment";

    export default {
        data: function () {
            return {
                render: 0,
                company: {
                    companyId: '',
                    logo: '',
                    date: '',
                    commercialRegistration: '',
                    businessLicence: '',
                    civilDefenceLicense: '',
                    cctvLicence: '',
                    companyAttachments: []
                },
                show: false,
                currentPage: 1,
                pageCount: '',
                rowCount: '',
                search: ''
            }
        },

        methods: {
            closeModal: function () {
                this.show = false;
                this.GetCompanyData(this.search, 1);
            },

            DownloadAttachment(path) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var ext = path.split('.')[1];
                root.$https.get('/Contact/DownloadFile?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` }, responseType: 'blob' })
                    .then(function (response) {
                        const url = window.URL.createObjectURL(new Blob([response.data]));
                        const link = document.createElement('a');
                        link.href = url;
                        link.setAttribute('download', 'file.' + ext);
                        document.body.appendChild(link);
                        link.click();
                    });
            },


            GetCompanyData: function (search, currentPage) {
                var root = this;
                var url = '/Company/GetCompanyAttachments?searchTerm=' + search + '&pageNumber=' + currentPage;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get(url, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data.results.length > 0) {

                        root.company = {
                            companyId: response.data.results[0].companyId,
                            logo: "",
                            commercialRegistration: "",
                            businessLicence: "",
                            civilDefenceLicense: "",
                            cctvLicence: "",
                            companyAttachments: response.data.results
                        };
                    }
                    else {
                        root.company = {
                            companyId: '00000000-0000-0000-0000-000000000000',
                            logo: '',
                            commercialRegistration: '',
                            businessLicence: '',
                            civilDefenceLicense: '',
                            cctvLicence: '',
                            companyAttachments: [{
                                logo: '',
                                commercialRegistration: '',
                                businessLicence: '',
                                civilDefenceLicense: '',
                                cctvLicence: '',
                            }]
                        }
                    }

                    root.pageCount = response.data.pageCount;
                    root.rowCount = response.data.rowCount;

                });
            }

        },
        watch: {
            search: function (val) {
                this.GetEmployeeData(val, 1);
            }
        },
        filters: {
            formatDate: function (value) {
                return moment(value).format("DD MMM yyyy hh:mm");
            }
        },
        mounted: function () {
            this.GetCompanyData(this.search, 1);
        }
    }
</script>
