<template>
    <div class="row" v-if="isValid('CanRestoreData')">
        <div class="row">
            <div class="col-sm-12">
                <div class="page-title-box">
                    <div class="row">
                        <div class="col">
                            <h4 class="page-title">{{$t('Backup.BackupDatabase')}}</h4>
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">{{$t('Backup.Home')}}</a></li>
                                <li class="breadcrumb-item active">{{$t('Backup.BackupDatabase')}}</li>
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
                        <h4 class="card-title text-center DayHeading"> {{$t('Backup.BackupDatabase')}}</h4>
                    </div>
                    <div class="card-body">
                        <div class="col-md-12 col-lg-12 pb-3">
                            <div class="col-sm-12">
                                <label><span>{{$t('Backup.BackupPath')}} :<span
                                class="text-danger"> *</span></span></label>
                                <div class="form-group">
                                    <input class="form-control" v-model="path" type="text"
                                        placeholder="Example: C:\FolderName"/>
                                </div>
                            </div>
                            <div class="form-group text-center">
                                <a href="javascript:void(0)" class="btn btn-primary  " v-on:click="backupData"
                                    :disabled="loading1 || path == ''"> {{$t('Backup.BackupDatabase')}}</a>
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
            oldPath: ''
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
                        root.oldPath = root.path;
                        root.loading1 = false;

                    }
                });
        },

        backupData: function () {
            var root = this;
            this.loading1 = true;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            root.loading1 = true;
            var isNewPath = false;
            if (this.path != this.oldPath) {
                isNewPath = true;
            }
            root.$https
                .get('/System/BackUp?path=' + this.path + '&isNewPath=' + isNewPath, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.status == 200) {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Success!' : 'النجاح!',
                            text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Backup save successfully' : 'حفظ النسخ الاحتياطي بنجاح',
                            type: 'success',
                            confirmButtonClass: "btn btn-Success",
                            buttonStyling: false,
                            icon: 'success'

                        });
                    } else {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            type: 'error',
                            confirmButtonClass: "btn btn-Success",
                            buttonStyling: false,
                            icon: 'error'

                        });
                    }

                    root.loading1 = false;
                }, (error) => {
                    root.loading1 = false;

                    root.$swal({
                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                        text: error,
                        type: 'error',
                        confirmButtonClass: "btn btn-Success",
                        buttonStyling: false,
                        icon: 'error'

                    });
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