<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('ImportProduct.ImportProduct') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('color.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('ImportProduct.ImportProduct') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="Download" href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center fa fa-download"></i>
                                    {{ $t('ImportProduct.DownloadTemplate') }}
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
                <div class="card-body">
                    <div class="row" :key="render">
                        <div class="col-lg-12 ml-auto mr-auto">
                            <label>{{ $t('ImportProduct.File(csvonly)') }}</label>
                            <!--<b-form-file v-model="file1"
                                         id="uplaodfile"
                                         :no-drop="true"
                                         accept=".xlsx"
                                         :state="Boolean(file1)"
                                         @update:modelValue="validFile"
                                         v-bind:placeholder="$t('ChooseFile')"></b-form-file>-->
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12 mt-3 " v-bind:class="$i18n.locale == 'en' ? 'text-right' : 'text-left'">
                            <button class="btn btn-outline-primary mx-1 "
                                    v-bind:class="{'disabled': (file1 == null ||  loading==true) }"
                                    @click="uploadFile">
                                <i class="nc-icon nc-cloud-upload-94"></i> {{ $t('ImportProduct.Upload') }}
                            </button>
                            <button class="btn btn-danger   mr-2"
                                    v-on:click="onCancel">
                                {{ $t('ImportProduct.Cancel') }}
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
                fileInterval: ''
            }
        },
        methods: {
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },
            onCancel: function () {
                this.$router.push('/products');
            },
            Download: function () {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var path = '/Template/ReparingOrderTemplate.xlsx'
                var ext = path.split('.')[1];
                root.$https.get('/Product/DownloadFileAsync?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` }, responseType: 'blob' })
                    .then(function (response) {

                        const url = window.URL.createObjectURL(new Blob([response.data]));
                        const link = document.createElement('a');
                        link.href = url;
                        link.setAttribute('download', 'Reparing Order.' + ext);
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
                fileData.append("file", this.file1);
                root.loding == true;
                root.$https.post('/Product/UploadFilesForImportReparingOrder', fileData, { headers: { "Authorization": `Bearer ${token}` } });
                root.$swal({
                    title: 'Import! File has been Import successfully',
                    text: "Data has import successfully",
                    type: 'success',
                    icon: 'success',
                    showConfirmButton: false,
                    timer: 800,
                    timerProgressBar: true,
                });
                root.ErrorFileInterval();
                root.$router.push('/ReparingOrder')
            },
            validFile: function (file) {
                if (file != null) {
                    var ext = file.name.split(".");
                    var validExt = ['xlsx'];
                    //check if file is not have recommended extension
                    if (validExt.indexOf(ext[1].toLowerCase()) === -1) {
                        alert("file " + file.name + " does not have required extension.");
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
                var path = '/Template/Template Error File.xlsx'
                var ext = path.split('.')[1];
                root.$https.get('/Product/DownloadErrorFileAsync?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` }, responseType: 'blob' })
                    .then(function (response) {
                        //eslint-disable-line
                        if (response.data.size > 14) {
                            //eslint-disable-line
                            const url = window.URL.createObjectURL(new Blob([response.data]));
                            const link = document.createElement('a');
                            link.href = url;
                            link.setAttribute('download', 'Product Template Error.' + ext);
                            document.body.appendChild(link);
                            link.click();

                            root.$https.get('/Product/DeleteErrorFileAsync?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` } });

                            root.ClearErrorFileInterval();
                        }
                    });
            },

            ErrorFileInterval: function () {
                var root = this;
                this.fileInterval = setInterval(() => {
                    root.DownloadErrorFile();

                }, 30000);
            },
            ClearErrorFileInterval: function () {
                clearInterval(this.fileInterval)
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