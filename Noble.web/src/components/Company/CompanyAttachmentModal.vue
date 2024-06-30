<template>
    <modal :show="show" :modalLarge="true">
        <div class="modal-lg">
            <div style="margin-bottom:0px" class="card">
                <div class="modal-header">
                    <h5>Attachment</h5>
                </div>

                    <div class="card-body">
                        <div class="row">
                            <div class="col-sm-6">
                                <label>Logo :</label>
                                <div>
                                    <span>
                                        <input ref="imgupload5" type="file" id="file-input"
                                               @change="uploadImage('logo')"
                                               name="image" style="opacity:1;padding:25px">
                                    </span>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <label>Commercial Registration:</label>
                                <div>
                                    <span>
                                        <input ref="imgupload1" type="file" id="file-input"
                                               @change="uploadImage('commercialRegistration')"
                                               name="image" style="opacity:1;padding:25px">
                                    </span>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <label>Business License:</label>
                                <div>
                                    <span>
                                        <input ref="imgupload2" type="file" id="file-input"
                                               @change="uploadImage('businessLicence')" 
                                               name="image" style="opacity:1;padding:25px">
                                    </span>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <label>Civil Defence License :</label>
                                <div>
                                    <span>
                                        <input ref="imgupload3" type="file" id="file-input"
                                               @change="uploadImage('civilDefenceLicense')"
                                               name="image" style="opacity:1;padding:25px">
                                    </span>
                                </div>
                            </div>

                            <div class="col-sm-6">
                                <label>CCTV Licence :</label>
                                <div>
                                    <span>
                                        <input ref="imgupload4" type="file" id="file-input"
                                               @change="uploadImage('cctvLicence')"
                                               name="image" style="opacity:1;padding:25px">
                                    </span>
                                </div>
                            </div>

                            
                        </div>
                    </div>


                    <div class="modal-footer justify-content-right">
                        <div class="col-sm-12 text-right">
                            <button class="btn btn-primary  float-right mr-2" v-on:click="SaveCompanyAttachment()" :disabled="!isUpload">Update</button>
                        </div>
                        <button type="button" class="btn btn-secondary  mr-3 " v-on:click="$emit('close')">{{ $t('Company.Close') }}</button>

                    </div>
            </div>
            <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        </div>


    </modal>
</template>
<script>

    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default {
        props: ['show'],
          components: {
            Loading
        },
        data: function () {
            return {
                   loading: false,
                render: 0,
                company: {
                    companyId: '',
                    logo: '',
                    date:'',
                    commercialRegistration: '',
                    businessLicence: '',
                    civilDefenceLicense: '',
                    cctvLicence: '',
                    companyAttachments: []
                },
                isUpload:false
            }
        },

        methods: {
            SaveCompanyAttachment: function () {
                  this.loading = true;
                var root = this;
                var url = '/Company/SaveCompanyAttachment';
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https
                    .post(url, root.company, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(response => {

                        if (response.data != null) {
                            root.$swal({
                                title: "Success!",
                                text: "Saved Successfully.",
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 800,
                                timerProgressBar: true,
                                confirmButtonClass: "btn btn-success",
                                buttonsStyling: false
                            });

                            root.GetCompanyData();
                            root.$emit('close');
                        }
                    })
                    .catch(error => {
                        console.log(error)
                        root.errored = true
                    })
                    .finally(() => root.loading = false)
            },

            uploadImage(type) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                var file = null;
                if (type == 'commercialRegistration') {
                    file = this.$refs.imgupload1.files;
                }

                if (type == 'businessLicence') {
                    file = this.$refs.imgupload2.files;
                }

                if (type == 'civilDefenceLicense') {
                    file = this.$refs.imgupload3.files;
                }

                if (type == 'cctvLicence') {
                    file = this.$refs.imgupload4.files;
                }

                if (type == 'logo') {
                    file = this.$refs.imgupload5.files;
                }

                var fileData = new FormData();
                for (var k = 0; k < file.length; k++) {
                    fileData.append("files", file[k]);
                }

                root.isUpload = true;
                root.$https.post('/Company/UploadFilesAsync', fileData, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            if (type == 'commercialRegistration') {
                                root.company.commercialRegistration = response.data;
                            }

                            if (type == 'businessLicence') {
                                root.company.businessLicence = response.data;
                            }

                            if (type == 'civilDefenceLicense') {
                                root.company.civilDefenceLicense = response.data;
                            }

                            if (type == 'cctvLicence') {
                                root.company.cctvLicence = response.data;
                            }

                            if (type == 'logo') {
                                root.company.logo = response.data;
                            }
                        }
                    },
                        function () {
                            root.loading = false;
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                type: 'error',
                                confirmButtonClass: "btn btn-danger",
                                buttonsStyling: false
                            });
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
                            logo: response.data.results[0].logo,
                            commercialRegistration: response.data.results[0].commercialRegistration,
                            businessLicence: response.data.results[0].businessLicence,
                            civilDefenceLicense: response.data.results[0].civilDefenceLicense,
                            cctvLicence: response.data.results[0].cctvLicence,
                            date: "",
                            companyAttachments: response.data.results
                        };
                    }
                    else {
                        root.company = {
                            companyId: response.data.results[0].companyId,
                            logo: '',
                            commercialRegistration: '',
                            businessLicence: '',
                            civilDefenceLicense: '',
                            cctvLicence: '',
                            date: "",
                            companyAttachments: [{
                                logo: '',
                                commercialRegistration: '',
                                businessLicence: '',
                                civilDefenceLicense: '',
                                cctvLicence: '',
                            }]
                        }
                    }
                });
            }

        },

        created: function () {
            var root = this;

            if (this.$route.query.data != undefined) {
                this.company = this.$route.query.data;

                this.country = this.company.countryEnglish;
                if (this.company.companyAttachments.length > 0) {
                    this.company.companyAttachments.forEach(x => {
                        root.company.logo = x.logo;
                        root.company.commercialRegistration = x.commercialRegistration;
                        root.company.businessLicence = x.businessLicence;
                        root.company.date = x.date;
                        root.company.civilDefenceLicense = x.civilDefenceLicense;
                        root.company.cctvLicence = x.cctvLicence;
                        root.company.companyId = x.companyId;
                    })
                }
            } else {
                this.company.companyAttachments.push({
                    logo: '',
                    commercialRegistration: '',
                    businessLicence: '',
                    civilDefenceLicense: '',
                    date: "",
                    cctvLicence: '',
                })
            }
        },
        mounted: function () {
            this.GetCompanyData(this.search, 1);
        }
    }
</script>
