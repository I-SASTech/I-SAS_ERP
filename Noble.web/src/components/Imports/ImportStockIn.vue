<template>
    <div>
        <div class="col-lg-6 col-sm-6 ml-auto mr-auto">
            <div class="card ">
                <div class="card-header">
                    <div class="row"
                        v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                        <div class="col-md-6">
                            <h4 class="card-title "
                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'text-right'">{{
                                    $t('ImportStockIn.Import') }}</h4>
                        </div>
                        <div class="col-md-6">
                            <h6 class="info-text"
                                v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">
                                <a v-on:click="Download" class="btn btn-outline-primary  btn-sm" data-toggle="tooltip"
                                    data-placement="top" title="Download"><i class="fa fa-download"></i> {{
                                        $t('ImportStockIn.DownloadTemplate') }}</a>
                            </h6>
                        </div>
                    </div>
                </div>
                <div class="card-body ">
                    <div class="row">
                        <div class="col-lg-12 ml-auto mr-auto mb-2"
                            v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            <label>{{ $t('ImportStockIn.WareHouse') }} :<span class="text-danger"> *</span></label>
                            <div>
                                <warehouse-dropdown v-model="wareHouseId" />
                            </div>
                        </div>
                    </div>
                    <div class="row" :key="render">

                        <div class="col-lg-12 ml-auto mr-auto">
                            <label>{{ $t('ImportStockIn.File(csvonly)') }}</label>
                            <!--<b-form-file v-model="file1"
                                         id="uplaodfile"
                                         :no-drop="true"
                                         accept=".xlsx"
                                         :state="Boolean(file1)"
                                         @update:modelValue="validFile"
                                         v-bind:placeholder="$t('ImportStockIn.ChooseFile')"></b-form-file>-->
                        </div>
                    </div>
                    <div class="row" v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-right' : 'text-left'">

                        <div class="col-lg-12 mt-3 ">
                            <button class="btn btn-primary  "
                                v-bind:class="{ 'disabled': ((file1 == null || loading == true) || (wareHouseId == null)) }"
                                @click="uploadFile">
                                <i class="nc-icon nc-cloud-upload-94"></i> {{ $t('ImportStockIn.Upload') }}
                            </button>
                            <button class="btn btn-danger   mr-2" v-on:click="onCancel">
                                {{ $t('ImportStockIn.Cancel') }}
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <loading :name="loading" v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
    </div>
</template>
<script>
    /*import { BFormFile } from 'bootstrap-vue';*/
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    
    export default {
         components: {
            Loading
        },
        data: function () {
            return {
                file1: null,
                loading: false,
                render: 0,
                year: '',
                fileInterval: '',
                wareHouseId: null,
                count:0
            }
        },
        methods: {
            onCancel: function () {
                this.$router.push('/stockValue?formName=StockIn');
            },
            Download: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.loading = true;
                var path = '/Template/Stock In Template.xlsx'
                var ext = path.split('.')[1];
                root.$https.get('/Product/DownloadStockFileAsync?filePath=' + path + '&warehouseId=' + this.wareHouseId, { headers: { "Authorization": `Bearer ${token}` }, responseType: 'blob' })
                    .then(function (response) {

                    const url = window.URL.createObjectURL(new Blob([response.data]));
                    const link = document.createElement('a');
                    link.href = url;
                    link.setAttribute('download', 'Stock In Template.' + ext);
                    document.body.appendChild(link);
                    link.click();
                    root.loading = false;
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
            if (this.wareHouseId != null) {
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
                fileData.append("WareHouseId", root.wareHouseId);
                root.loding == true;
                root.$https.post('/Product/UploadFilesForImportStock', fileData, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.DownloadErrorFile();
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
                            }
                            root.loading = false;
                            root.$router.push({
                                path: '/stockValue',
                                query: {
                                    formName: 'StockIn',
                                }
                            });
                        }
                    });


                //root.$router.push('/ProductStockValue')

            }
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
            var path = '/Template/Template Stock Error File.xlsx'
            var ext = path.split('.')[1];
            root.$https.get('/Product/DownloadErrorFileAsync?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` }, responseType: 'blob' })
                .then(function (response) {
                    //eslint-disable-line
                    if (response.data.size > 14) {
                        //eslint-disable-line
                        const url = window.URL.createObjectURL(new Blob([response.data]));
                        const link = document.createElement('a');
                        link.href = url;
                        link.setAttribute('download', 'Stock Error File.' + ext);
                        document.body.appendChild(link);
                        link.click();

                        root.$https.get('/Product/DeleteErrorFileAsync?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` } });

                        root.ClearErrorFileInterval();
                    }
                });
        },

    },
    mounted: function () {

    }
}
</script>

<style scoped>.custom-file-label span {
    margin-right: 65px;
}</style>