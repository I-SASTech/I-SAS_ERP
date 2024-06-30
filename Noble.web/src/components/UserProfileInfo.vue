<template>
    <div>
        <table>
            <thead>
                <tr>
                    <td>UserID</td>
                    <td>{{UserID}}</td>
                </tr>
                <tr>
                    <td>UserName</td>
                    <td>{{UserName}}</td>
                </tr>
                <tr>
                    <td>FullName</td>
                    <td>{{FullName}}</td>
                </tr>
                <tr>
                    <td>RoleName</td>
                    <td>{{RoleName}}</td>
                </tr>
                <tr>
                    <td>CompanyID</td>
                    <td>{{CompanyID}}</td>
                </tr>
            </thead>
        </table>
    </div>
</template>
<script>
  
    export default {
       
        data: function () {
            return {
                CompanyName: '',
                UserID: '',
                UserName: '',
                FullName: '',
                RoleName:''

            }
        },
       
        mounted: function () {

            if (this.$store.isAuthenticated) {
                this.UserID = localStorage.getItem('UserID');
                this.UserName = localStorage.getItem('UserName');
                this.FullName = localStorage.getItem('FullName');
                this.RoleName = localStorage.getItem('RoleName');
                this.CompanyID = localStorage.getItem('CompanyID');
            }
            else {
                this.$router.push('/')
            }
        }
    }
    </script>
    