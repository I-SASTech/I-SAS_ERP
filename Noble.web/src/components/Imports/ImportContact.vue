<template>
    <div v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
        <div class="col-lg-6 col-sm-6 ml-auto mr-auto">
            <div class="card ">
                <div class="card-header">
                    <div class="row">
                        <div class="col-md-6">
                            <h4 v-if="isCustomer" class="card-title DayHeading" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">{{ $t('ImportContact.ImportCustomer') }}</h4>
                            <h4 v-else class="card-title DayHeading" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">{{ $t('ImportContact.ImportSupplier') }}</h4>
                        </div>
                        <div class="col-md-6">
                            <h6 class="info-text" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                <a v-on:click="Download" class="btn btn-outline-primary  btn-sm" data-toggle="tooltip" data-placement="top" title="Download"><i class="fa fa-download"></i> {{ $t('ImportContact.DownloadTemplate') }}</a>
                            </h6>
                        </div>
                    </div>
                </div>
                <div class="card-body ">

                    <div class="row" :key="render">
                        <div class="col-lg-12 ml-auto mr-auto">
                            <label>{{ $t('ImportContact.File(csvonly)') }}</label>
                            <!--<b-form-file v-model="file1"
                                         id="uplaodfile"
                                         :no-drop="true"
                                         accept=".xlsx"
                                         :state="Boolean(file1)"
                                         @update:modelValue="validFile"
                                         v-bind:placeholder="$t('ImportContact.ChooseFile')"></b-form-file>-->
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12 mt-3 " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                            <button class="btn btn-primary  "
                                    v-bind:class="{'disabled': (file1 == null ||  loading==true) }"
                                    @click="uploadFile">
                                <i class="nc-icon nc-cloud-upload-94"></i> {{ $t('ImportContact.Upload') }}
                            </button>
                            <button class="btn btn-danger   mr-2"
                                    v-on:click="onCancel">
                                {{ $t('ImportContact.Cancel') }}
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
    /*import { BFormFile } from 'bootstrap-vue';*/
    export default {
        //components: {
        //    BFormFile
        //},
        data: function () {
            return {
                file1: null,
                loading: false,
                render: 0,
                year: '',
                fileInterval: '',
                isCustomer: false,
                templateName : ''
            }
        },
        created: function () {
            
            this.isCustomer = this.$route.query.data == "true"?true:false
        },
        methods: {
            onCancel: function () {
                if (this.isCustomer) {
                    this.$router.push('/Customer');
                }
                else {
                    this.$router.push('/supplier');
                }
            },
            Download: function () {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var path = ''
                if (this.isCustomer) {
                    root.templateName = ''
                    path = '/Template/Customer Template.xlsx';
                    root.templateName = 'Customer Template.'
                }
                else {
                    root.templateName = ''
                    path = '/Template/Supplier Template.xlsx'
                    root.templateName = 'Supplier Template.'
                }
                var ext = path.split('.')[1];
                root.$https.get('/Contact/DownloadFileAsync?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` }, responseType: 'blob' })
                    .then(function (response) {

                        const url = window.URL.createObjectURL(new Blob([response.data]));
                        const link = document.createElement('a');
                        link.href = url;
                        link.setAttribute('download', root.templateName + ext);
                        document.body.appendChild(link);
                        link.click();
                    });
            },

            onlyNumber: function ($event) {
                //console.log($event.keyCode); //keyCodes value
                let keyCode = ($event.keyCode ? $event.keyCode : $event.which);
                if ((keyCode < 48 || keyCode > 57) && keyCode !== 46) { // 46 is dot
                    $event.preventDefault();
                }
            },
            SelectedValue: function (value) {
                this.year = value;
            },
            uploadFile: function () {

                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var fileData = new FormData();
                //eslint-disable-line
                // convert file into FormData object
                fileData.append("File", this.file1);
                fileData.append("IsContact", root.isCustomer);
                root.loding == true;
                root.$https.post('/Contact/UploadFilesForImport', fileData, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        if (response.data == 'File has been imported') {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Import!' : 'استورد!', 
                                text: response.data,
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: true,
                            });
                        }
                        else {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Import!' : 'استورد!', 
                                text: response.data,
                                type: 'error',
                                icon: 'error',
                                showConfirmButton: true,
                            });
                            root.DownloadErrorFile();
                        }

                        if (root.isCustomer) {
                            root.$router.push('/Customer');
                        }
                        else {
                            root.$router.push('/supplier');
                        }

                    }
                }).catch(error => {
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
                });
                
            },
            validFile: function (file) {
                if (file != null) {
                    var ext = file.name.split(".");
                    var validExt = ['xlsx'];
                    //check if file is not have recommended extension
                    if (validExt.indexOf(ext[1].toLowerCase()) === -1) {
                        //alert("file " + file.name + " does not have required extension.");
                        this.file1 = null;
                    }
                }
            },


            DownloadErrorFile: function () {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                var path = ''
                if (this.isCustomer) {
                    root.templateName = ''
                    path = '/Template/Customer Template Error.xlsx';
                    root.templateName = 'Customer Template Error.'
                }
                else {
                    root.templateName = ''
                    path = '/Template/Supplier Template Error.xlsx'
                    root.templateName = 'Supplier Template Error.'
                }
                var ext = path.split('.')[1];
                root.$https.get('/Contact/DownloadErrorFileAsync?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` }, responseType: 'blob' })
                    .then(function (response) {
                        //eslint-disable-line
                        if (response.data.size > 14) {
                            //eslint-disable-line
                            const url = window.URL.createObjectURL(new Blob([response.data]));
                            const link = document.createElement('a');
                            link.href = url;
                            link.setAttribute('download', root.templateName+ ext);
                            document.body.appendChild(link);
                            link.click();

                            root.$https.get('/Contact/DeleteErrorFileAsync?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` } });

                            root.ClearErrorFileInterval();
                        }
                    });
            },

            
        },
        mounted: function () {

        }
    }
</script>

<style scoped>
    .custom-file-label span {
        margin-right: 65px;
    }
</style>