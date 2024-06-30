<template>
    <div class="row" v-if="isValid('CanRestoreData')">
        <div class="row">
            <div class="col-sm-12">
                <div class="page-title-box">
                    <div class="row">
                        <div class="col">
                            <h4 class="page-title">{{ $t('RestoreDatabase.RestoreDatabase') }}</h4>
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('RestoreDatabase.Home') }}</a></li>
                                <li class="breadcrumb-item active">{{ $t('RestoreDatabase.RestoreDatabase') }}</li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row justify-content-center align-items-center">
            <div class="col-lg-6 ">
            <div class="card">
                <div class="card-header">
                    <h4 class="card-title text-center DayHeading"> {{ $t('RestoreDatabase.RestoreDatabase') }}</h4>
                </div>
                <div class="card-body">
                    <div class="">
                        <div class="mb-3">
                            <div>
                                <label><span>{{ $t('RestoreDatabase.BackupPath') }} :<span class="text-danger"> *</span> <small>({{ $t('RestoreDatabase.WithoutFileName') }})</small></span></label>
                                <div class="form-group">
                                    <input class="form-control" v-model="path" type="text"
                                        placeholder="Example: C:\FolderName" />
                                </div>
                            </div>

                            <div class="col-sm-12 form-group ">
                                <div :key="renderImg">
                                    <div class="input-group">
                                        <input type="file" class="form-control" id="inputGroupFile01"
                                            @change="saveFile">
                                    </div>

                                </div>
                            </div>
                        </div>

                        <div class="form-group text-center">
                            <a href="javascript:void(0)" class="btn btn-primary  " v-on:click="restoreData"
                               :disabled="loading1 || path == ''">{{ $t('RestoreDatabase.RestoreDatabase') }}</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </div>
        

    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>

    <!-- <div class="row" v-if="isValid('CanRestoreData')">
        <div class="col-lg-6 col-sm-12 ml-auto mr-auto">
            <div class="card ">
                <div class="card-header">
                    <h4 class="card-title text-center DayHeading"> Restore Database</h4>
                    <div class="row">
                        <div class="col-md-12 col-lg-12 pb-3">
                            <div class="mb-3">
                                <div v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                    <label><span>Backup Path *: <small>(without file name)</small></span></label>
                                    <div class="form-group">
                                        <input class="form-control" v-model="path" type="text" placeholder="Example: C:\FolderName" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" />
                                    </div>
                                </div>

                                <div class="custom-file">
                                    <input type="file" class="file-input" @change="saveFile" id="inputGroupFile01">
                                    <label class="custom-file-label" for="inputGroupFile01">{{fileName}}</label>
                                </div>
                            </div>

                            <div class="form-group text-center">


                                <a href="javascript:void(0)" class="btn btn-primary  " v-on:click="restoreData" :disabled="loading1 || path == ''">Restore Database</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <loading :name="loading1" v-model:active="loading1"
                     :can-cancel="true"
                     :is-full-page="true"></loading>

        </div>

    </div>
    <div v-else>  <acessdenied></acessdenied></div> -->

</template>

<script>

import clickMixin from '@/Mixins/clickMixin'
export default {
    mixins: [clickMixin],
    data: function () {
        return {
            loading1: false,
            fileName: 'Choose file',
            path: '',
        }
    },
    methods: {
        getBackUpPath: function () {
            var root = this;
            this.loading1 = true;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            root.loading1 = true;

            root.$https
                .get('/System/GetBackUpPath', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.status == 200) {

                        root.path = response.data;
                        root.loading1 = false;

                    }
                });
        },
        saveFile(e) {
            if (e.currentTarget.files.length == 0) {
                this.fileName = 'choose file';
                return;
            }

            var name = e.currentTarget.files[0].name;
            if (name.split('.')[1] != "bak") {
                this.$swal({
                    title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Error!' : 'خطأ',
                    text: "Please upload bak file.",
                    type: 'error',
                    confirmButtonClass: "btn btn-error",
                    buttonStyling: false,
                    icon: 'error'

                });
            }
            this.fileName = name;

            console.log(e);
        },
        restoreData: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.loading1 = true;
            root.$https
                .get('/System/Restore?fileName=' + this.fileName + '&path=' + this.path, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.status == 200) {
                        root.$swal({
                            title: "Success!",
                            text: "Database restore successfully",
                            type: 'error',
                            confirmButtonClass: "btn btn-Success",
                            buttonStyling: false,
                            icon: 'success'

                        });
                    }

                    root.loading1 = false;

                }, (e) => {
                    root.loading1 = false;
                    console.log(e);
                });
        },
    },
    created: function () {
        this.$emit('update:modelValue', this.$route.name);
    },
    mounted: function () {
        this.getBackUpPath();
    }
}
</script>